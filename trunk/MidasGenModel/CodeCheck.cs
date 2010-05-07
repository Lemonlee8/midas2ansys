using System;
using System.Collections.Generic;
using System.Text;
using MidasGenModel.model;
using System.IO;

namespace MidasGenModel.Design
{
    /// <summary>
    /// 截面设计基本类
    /// </summary>
    public class CodeCheck
    {
        /// <summary>
        /// 计算压弯组合构件强度 GB 50017-2003 式(5.2.1) 
        /// 注：目前默认支持单位为：N,m
        /// </summary>
        /// <param name="NL">截面内力</param>
        /// <param name="Sec">截面参数</param>
        /// <param name="DPs">设计参数</param>
        /// <returns>强度应力值</returns>
        public static double CalStrength_YW(SecForce NL,BSections Sec,DesignParameters DPs )
        {
            double Res1,Res2,Res3,Res4,RES;//返回结果

            double Wy=Sec.Iyy/Math.Max(Sec.CzM,Sec.CzP);//抗弯截面模量
            double Wz=Sec.Izz/Math.Max(Sec.CyM,Sec.CzP);//抗弯截面模量
            double Wny = Wy * DPs.Ratio_Anet;//净截面模量
            double Wnz = Wz * DPs.Ratio_Anet;//净截面模量
            double My=Math.Abs(NL.My);//弯矩，取绝对值
            double Mz=Math.Abs(NL.Mz);

            Res1 = NL.N / (Sec.Area * DPs.Ratio_Anet) +
                My / (DPs.Gamma_y * Wny) + Mz / (DPs.Gamma_z* Wnz);
            Res2 = NL.N / (Sec.Area * DPs.Ratio_Anet) -
                My / (DPs.Gamma_y * Wny) - Mz / (DPs.Gamma_z * Wnz);
            Res3 = NL.N / (Sec.Area * DPs.Ratio_Anet) -
                My / (DPs.Gamma_y * Wny) + Mz / (DPs.Gamma_z * Wnz);
            Res4 = NL.N / (Sec.Area * DPs.Ratio_Anet) +
                My / (DPs.Gamma_y * Wny) -Mz / (DPs.Gamma_z * Wnz);

            RES = Math.Max(Math.Abs(Res1), Math.Abs(Res2));
            RES = Math.Max(Math.Abs(Res3), RES);
            RES = Math.Max(Math.Abs(Res4), RES);
            return RES;
        }

        /// <summary>
        /// 计算压弯组合构件稳定性 GB 50017-2003 式(5.2.5-1) (5.2.5-2)
        /// 注：目前默认支持单位为：N,m
        /// </summary>
        /// <param name="NL">截面内力</param>
        /// <param name="Sec">截面参数</param>
        /// <param name="DPs">设计参数</param>
        /// <param name="E">材料的弹性模量</param>
        /// <returns>稳定验算应力值</returns>
        public static double CalStability_YW(SecForce NL, BSections Sec, DesignParameters DPs,
            double E)
        {
            double Res1, Res2,RES;//分别对应(5.2.5-1) (5.2.5-2)式结果

            double Wy = Sec.Iyy / Math.Max(Sec.CzM, Sec.CzP);//抗弯截面模量
            double Wz = Sec.Izz / Math.Max(Sec.CyM, Sec.CzP);//抗弯截面模量
            double N = NL.N;//轴向力
            double My = Math.Abs(NL.My);//弯矩，取绝对值
            double Mz = Math.Abs(NL.Mz);

            //参数
            double N_ey = Math.Pow(Math.PI, 2) * E * Sec.Area / (1.1 * Math.Pow(DPs.Lemda_y, 2));
            double N_ez = Math.Pow(Math.PI, 2) * E * Sec.Area / (1.1 * Math.Pow(DPs.Lemda_z, 2));

            double Ref_Ny = 1;//与轴压力有关的参数
            double Ref_Nz = 1;
            if (N < 0)//如为轴压力对参数进行调整
            {
                Ref_Ny = 1 - 0.8 * Math.Abs(N) / N_ey;
                Ref_Nz = 1 - 0.8 * Math.Abs(N) / N_ez;
            }

            //进行功能计算，注意弯矩后两项应为-号
            Res1 = N / (DPs.Phi_y * Sec.Area) - DPs.Belta_my * My / (DPs.Gamma_y * Wy * Ref_Ny) -
                DPs.Yita * DPs.Belta_tz * Mz / (DPs.Phi_bz * Wz);
            Res2 = N / (DPs.Phi_z * Sec.Area) -DPs.Yita * DPs.Belta_ty * My / (DPs.Phi_by * Wy)
                - DPs.Belta_mz * Mz / (DPs.Gamma_z * Wz * Ref_Nz);

            RES = Math.Max(Math.Abs(Res1), Math.Abs(Res2));
            return RES;
        }

        /// <summary>
        /// 输出单个单元的验算结果表格
        /// </summary>
        /// <param name="mm">模型对像</param>
        /// <param name="FileOut">输出文件路径</param>
        /// <param name="iElem">单元号</param>
        public static void WriteElemCheckRes(ref Bmodel mm, string FileOut, int iElem)
        {
            FileStream stream = File.Open(FileOut, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("单元号\t截面位置\t荷载组合\tN(kN)\tMy(kN*m)\tMz(kN*m)\t强度(MPa)\t稳定(MPa)\t应力比");

            List<string> coms = mm.LoadCombTable.ComSteel;
            foreach (string com in coms)
            {
                FrameElement ele =mm.elements[iElem] as FrameElement;
                //先进行单元内力组合
                ElemForce EFcom = mm.CalElemForceComb(mm.LoadCombTable[com], iElem);
           
                //计算强度
                double Strength_i=CalStrength_YW(EFcom.Force_i,
                    mm.sections[ele.iPRO],
                    ele.DPs);//i截面计算强度
                double Strength_2 = CalStrength_YW(EFcom.Force_48,
                    mm.sections[ele.iPRO],
                    ele.DPs);
                double Strength_j = CalStrength_YW(EFcom.Force_j,
                    mm.sections[ele.iPRO],
                    ele.DPs);
                //计算稳定性强度
                double Stability_i = CalStability_YW(EFcom.Force_i, mm.sections[ele.iPRO],
                    ele.DPs, mm.mats[ele.iMAT].Elast);
                double Stability_2 = CalStability_YW(EFcom.Force_48, mm.sections[ele.iPRO],
                    ele.DPs, mm.mats[ele.iMAT].Elast);
                double Stability_j = CalStability_YW(EFcom.Force_j, mm.sections[ele.iPRO],
                    ele.DPs, mm.mats[ele.iMAT].Elast);

                /*单位转换*/
                Strength_i = Strength_i / 1000000;//单元转为MPa
                Strength_2 = Strength_2 / 1000000;
                Strength_j = Strength_j / 1000000;
                Stability_i = Stability_i / 1000000;
                Stability_2 = Stability_2 / 1000000;
                Stability_j = Stability_j / 1000000;

                EFcom = EFcom.Mutiplyby(0.001);//转为KN，m

                double Ratio = Math.Max( Strength_i,Stability_i) / ele.DPs.fy;
                double Ratio_2 = Math.Max(Strength_2,Stability_2) / ele.DPs.fy;
                double Ratio_j = Math.Max(Strength_j,Stability_j) / ele.DPs.fy;

                writer.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", iElem.ToString(), "I", com,
                    EFcom.Force_i.N.ToString("0.0"), EFcom.Force_i.My.ToString("0.0"),
                    EFcom.Force_i.Mz.ToString("0.0"), Strength_i.ToString("0.0"),
                    Stability_i.ToString("0.0"),Ratio.ToString("0.00"));
                writer.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", iElem.ToString(), "1/2", com,
                    EFcom.Force_48.N.ToString("0.0"), EFcom.Force_48.My.ToString("0.0"),
                    EFcom.Force_48.Mz.ToString("0.0"), Strength_2.ToString("0.0"),
                    Stability_2.ToString("0.0"), Ratio_2.ToString("0.00"));
                writer.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", iElem.ToString(), "J", com,
                    EFcom.Force_j.N.ToString("0.0"), EFcom.Force_j.My.ToString("0.0"),
                    EFcom.Force_j.Mz.ToString("0.0"), Strength_j.ToString("0.0"),
                    Stability_j.ToString("0.0"), Ratio_j.ToString("0.00"));

            }

            writer.Close();
            stream.Close();
        }
    }
    /// <summary>
    /// 钢结构主要设计参数类
    /// </summary>
    [Serializable]
    public class DesignParameters
    {
        private double _Ratio_Anet;//净毛面积比
        private double _Gamma_y;//截面塑性发展系数
        private double _Gamma_z;//截面塑性发展系数
        private double _Belta_my, _Belta_mz, _Belta_ty, _Belta_tz;//等效弯矩系数
        private double _Phi_by,_Phi_bz;//受弯构件整体稳定性系数
        private double _Phi_y, _Phi_z;//受压构件稳定系数
        private double _lemda_y, _lemda_z;//长细比
        private double _lk_y, _lk_z;//计算长度系数
        private double _Yita;//截面影响系数GB50017-2003 P48 ：闭口截面取0.7,其它截面取1.0

        private double _fy;//抗拉，抗压强度设计值

        #region 属性
        /// <summary>
        /// 净毛面积比
        /// </summary>
        public double Ratio_Anet
        {
            get { return _Ratio_Anet; }
            set { _Ratio_Anet = value; }
        }
        /// <summary>
        /// 截面塑性发展系数
        /// </summary>
        public double Gamma_y
        {
            get { return _Gamma_y; }
            set { _Gamma_y = value; }
        }
        /// <summary>
        /// 截面塑性发展系数
        /// </summary>
        public double Gamma_z
        {
            get { return _Gamma_z; }
            set { _Gamma_z = value; }
        }

        /// <summary>
        /// 等效弯矩系数
        /// </summary>
        public double Belta_my
        {
            get { return _Belta_my; }
            set { _Belta_my = value; }
        }
        /// <summary>
        /// 等效弯矩系数
        /// </summary>
        public double Belta_mz
        {
            get { return _Belta_mz; }
            set { _Belta_mz = value; }
        }
        /// <summary>
        /// 等效弯矩系数
        /// </summary>
        public double Belta_ty
        {
            get { return _Belta_ty; }
            set { _Belta_ty = value; }
        }
        /// <summary>
        /// 等效弯矩系数
        /// </summary>
        public double Belta_tz
        {
            get { return _Belta_tz; }
            set { _Belta_tz = value; }
        }

        /// <summary>
        /// 受弯构件整体稳定性系数
        /// </summary>
        public double Phi_by
        {
            get { return _Phi_by; }
            set { _Phi_by = value; }
        }
        /// <summary>
        /// 受弯构件整体稳定性系数
        /// </summary>
        public double Phi_bz
        {
            get { return _Phi_bz; }
            set { _Phi_bz = value; }
        }
        /// <summary>
        /// 受压构件稳定系数
        /// </summary>
        public double Phi_y
        {
            get { return _Phi_y; }
            set { _Phi_y = value; }
        }
        /// <summary>
        /// 受压构件稳定系数
        /// </summary>
        public double Phi_z
        {
            get { return _Phi_z; }
            set { _Phi_z = value; }
        }
        /// <summary>
        /// 计算长度系数
        /// </summary>
        public double Lemda_y
        {
            get { return _lemda_y; }
            set { _lemda_y = value; }
        }
        /// <summary>
        /// 计算长度系数
        /// </summary>
        public double Lemda_z
        {
            get { return _lemda_z; }
            set { _lemda_z = value; }
        }
        /// <summary>
        /// 计算长度系数
        /// </summary>
        public double Lk_y
        {
            get { return _lk_y; }
            set { _lk_y = value; }
        }
        /// <summary>
        /// 计算长度系数
        /// </summary>
        public double Lk_z
        {
            get { return _lk_z; }
            set { _lk_z = value; }
        }
        /// <summary>
        /// 钢材强度设计值（MPa）
        /// </summary>
        public double fy
        {
            get { return _fy; }
            set { _fy = value; }
        }

        /// <summary>
        /// 截面影响系数GB50017-2003 P48 ：闭口截面取0.7,其它截面取1.0
        /// </summary>
        public double Yita
        {
            get { return _Yita; }
            set { _Yita = value; }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DesignParameters()
        {
            _Ratio_Anet = 0.85;
            _Gamma_y = 1.0;
            _Gamma_z = 1.0;
            _Belta_my = 1.0;
            _Belta_mz = 1.0;
            _Belta_ty = 1.0;
            _Belta_tz = 1.0;
            _Phi_by = 1;
            _Phi_bz = 1;
            _Phi_y = 1;
            _Phi_z = 1;
            _lemda_y = 50;
            _lemda_z = 50;

            _fy = 295;
            _Yita = 1.0;
        }
    }
}
