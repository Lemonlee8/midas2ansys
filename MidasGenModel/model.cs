using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace MidasGenModel.model
{
    #region Model Info(模型特性类)

    /// <summary>
    /// 模型单位信息
    /// </summary>
    public class BUNIT
    {
        private string _Force;
        private string _Length;
        private string _Heat;
        private string _Temper;

        /// <summary>
        /// 力的单位：N、KN等
        /// </summary>
        public string Force
        {
            set { _Force = value; }
            get { return _Force; }
        }

        /// <summary>
        /// 长度单位：m、mm等
        /// </summary>
        public string Length
        {
            set { _Length = value; }
            get { return _Length; }
        }

        /// <summary>
        /// 热量单位：kJ等
        /// </summary>
        public string Heat
        {
            set { _Heat = value; }
            get { return _Heat; }
        }

        /// <summary>
        /// 温度单位：C等
        /// </summary>
        public string Temper
        {
            set { _Temper = value; }
            get { return _Temper; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BUNIT()
        {
            _Force = "N";
            _Length = "M";
            _Heat = "KJ";
            _Temper = "C";
        }
    }
    #endregion
    #region Load Class(荷载类)

    /// <summary>
    /// 荷载工况类型
    /// </summary>
    public enum LCType
    {
        /// <summary>
        /// 恒荷
        /// </summary>
        D,
        /// <summary>
        /// 活荷
        /// </summary>
        L,
        /// <summary>
        /// 风荷
        /// </summary>
        W,
        /// <summary>
        /// 地震荷载
        /// </summary>
        E,
        /// <summary>
        /// 屋面活荷
        /// </summary>
        LR,
        /// <summary>
        /// 雪荷
        /// </summary>
        S,
        /// <summary>
        /// 温度荷载
        /// </summary>
        T,
        /// <summary>
        /// 预应力荷载
        /// </summary>
        PS,
        /// <summary>
        /// 用户定义
        /// </summary>
        USER
    }
    /// <summary>
    ///加载方向 
    /// </summary>
    public enum DIR
    {
        /// <summary>
        /// 整体坐标X向
        /// </summary>
        GX,
        /// <summary>
        /// 整体坐标Y向
        /// </summary>
        GY,
        /// <summary>
        /// 整体坐标Z向
        /// </summary>
        GZ,
        /// <summary>
        /// 单元局部坐标X向
        /// </summary>
        LX,
        /// <summary>
        /// 单元局部坐标Y向
        /// </summary>
        LY,
        /// <summary>
        /// 单元局部坐标Z向
        /// </summary>
        LZ
    }
    /// <summary>
    /// 荷载工况类
    /// </summary>
    public class BLoadCase
    {
        /// <summary>
        /// 荷载工况名称
        /// </summary>
        public string LCName;
        /// <summary>
        /// 荷载工况类型
        /// </summary>
        public LCType LCType;
    }
    /// <summary>
    /// 荷载类
    /// </summary>
    public abstract class Load
    {
        protected string group;//组名
        protected string lc;//工况
        /// <summary>
        /// 荷载组名
        /// </summary>
        public abstract string Group
        {
            get;
            set;
        }
        /// <summary>
        /// 荷载工况
        /// </summary>
        public abstract string LC
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 节点荷载类
    /// </summary>
    public class BNLoad : Load
    {
        private int node;//节点号
        private double fx, fy, fz, mx, my, mz;
        /// <summary>
        /// 节点号
        /// </summary>
        public int iNode
        {
            get { return node; }
        }
        /// <summary>
        /// 沿x方向的力
        /// </summary>
        public double FX
        {
            get { return fx; }
            set { fx = value; }
        }
        /// <summary>
        /// 沿y方向的力
        /// </summary>
        public double FY
        {
            get { return fy; }
            set { fy = value; }
        }
        /// <summary>
        /// 沿z方向的力
        /// </summary>
        public double FZ
        {
            get { return fz; }
            set { fz = value; }
        }
        /// <summary>
        /// x向弯矩
        /// </summary>
        public double MX
        {
            get { return mx; }
            set { mx = value; }
        }
        /// <summary>
        /// y向弯矩
        /// </summary>
        public double MY
        {
            get { return my; }
            set { my = value; }
        }
        /// <summary>
        /// z向弯矩
        /// </summary>
        public double MZ
        {
            get { return mz; }
            set { mz = value; }
        }

        /// <summary>
        /// 重载抽象方法：Group
        /// </summary>
        public override string Group
        {
            get
            {
                return group;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                group = value;
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public override string LC
        {
            get
            {
                return lc;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                lc = value;
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        /// <summary>
        /// 构造函数：初始荷载值全为0
        /// </summary>
        /// <param name="n">节点号</param>
        public BNLoad(int n)
        {
            node = n;
            fx = 0;
            fy = 0;
            fz = 0;
            mx = 0;
            my = 0;
            mz = 0;
            group = null;
            lc = null;
        }

        /// <summary>
        /// 批量修改节点荷载数据
        /// </summary>
        /// <param name="data">节点数据数组：daouble[]{FX,FY,FZ,
        /// MX,MY,MZ}</param>
        public void putdata(double[] data)
        {
            //to do:....
        }
    }

    /// <summary>
    /// 自重荷载类
    /// </summary>
    public class BWeight : Load
    {
        private double gx, gy, gz;
        /// <summary>
        /// 重力加速度常数g=9.805
        /// </summary>
        private const double g = 9.805;//重力加速度值
        /// <summary>
        /// 重力加速度x方向因子
        /// </summary>
        public double Gx
        {
            get
            {
                return gx;
            }
            set
            {
                gx = value;
            }
        }

        /// <summary>
        /// 重力加速度y方向因子
        /// </summary>
        public double Gy
        {
            get
            {
                return gy;
            }
            set
            {
                gy = value;
            }
        }

        /// <summary>
        /// 重力加速度z方向因子
        /// </summary>
        public double Gz
        {
            get
            {
                return gz;
            }
            set
            {
                gz = value;
            }
        }


        /// <summary>
        /// 重力加速度x方向值
        /// </summary>
        public double ACELx
        {
            get
            {
                return gx * g;
            }
            set
            {
                gx = value / g;
            }
        }

        /// <summary>
        /// 重力加速度y方向值
        /// </summary>
        public double ACELy
        {
            get
            {
                return gy * g;
            }
            set
            {
                gy = value / g;
            }
        }

        /// <summary>
        /// 重力加速度z方向值
        /// </summary>
        public double ACELz
        {
            get
            {
                return gz * g;
            }
            set
            {
                gz = value / g;
            }
        }
        /// <summary>
        /// 重载抽象方法：Group
        /// </summary>
        public override string Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
            }
        }

        /// <summary>
        /// 重载抽象方法：LC
        /// </summary>
        public override string LC
        {
            get
            {
                return lc;
            }
            set
            {
                lc = value;
            }
        }
    }

    /// <summary>
    /// 梁单元荷载类
    /// </summary>
    public class BBLoad : Load
    {
        private int elem_num;//单元号
        private string cmd, type;
        private DIR dir;//加载方向
        private bool bproj;//是否投影

        private bool beccen;//是否偏心
        private DIR eccdir;//偏心方向
        private double i_end, j_end;//i端和j端的偏心荷载值
        private bool bj_end;//是否具有j端偏心荷载

        private double d1, p1, d2, p2, d3, p3, d4, p4;//荷载数据组

        /// <summary>
        /// 单元编号
        /// </summary>
        public int ELEM_num
        {
            get { return elem_num; }
            set { elem_num = value; }
        }

        /// <summary>
        /// BEAM代表梁单元荷载，TYPITAL代表标准梁单元荷载
        /// </summary>
        public string CMD
        {
            get { return cmd; }
            set { cmd = value; }
        }

        /// <summary>
        /// 梁单元荷载类型
        /// </summary>
        public string TYPE
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// 加载方向
        /// </summary>
        public DIR Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        /// <summary>
        /// 荷载是否投影
        /// </summary>
        public bool bPROJ
        {
            get { return bproj; }
            set { bproj = value; }
        }

        /// <summary>
        /// 荷载是否偏心
        /// </summary>
        public bool bECCEN
        {
            get { return beccen; }
            set { beccen = value; }
        }

        /// <summary>
        /// 偏心荷载方向
        /// </summary>
        public DIR EccDir
        {
            get { return eccdir; }
            set { eccdir = value; }
        }

        /// <summary>
        /// 荷载组名
        /// </summary>
        public override string Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
            }
        }

        /// <summary>
        /// 荷载工况名
        /// </summary>
        public override string LC
        {
            get
            {
                return lc;
            }
            set
            {
                lc = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BBLoad()
        {
            elem_num = 0;
            beccen = false;//不偏心
            bproj = false;//不投影
            cmd = "BEAM";
            type = "UNILOAD";
            beccen = false;
            dir = DIR.GX;
            bj_end = false;
        }

        /// <summary>
        /// 提取偏心单元荷载信息
        /// </summary>
        /// <param name="dataline">mgt文件梁单元荷载数据行</param>
        public void readEccenDataMgt(string dataline)
        {
            string[] temp = dataline.Split(new char[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries);
            string bEccen = temp[5].Trim();
            string Ecc_Dir = temp[6].Trim();

            if (bEccen == "YES")
            {
                bool bb = false;
                if (temp[9].Trim() == "YES")
                    bb = true;

                beccen = true;
                setEccenDir(Ecc_Dir);//设置偏心方向
                i_end = double.Parse(temp[7].Trim());
                j_end = double.Parse(temp[8].Trim());
                bj_end = bb;
            }
            else if (bEccen == "NO")
            {
                beccen = false;
                eccdir = DIR.GX;
                i_end = 0;
                j_end = 0;
                bj_end = false;
            }
        }
        /// <summary>
        /// 更新偏心荷载数据
        /// </summary>
        /// <param name="bEccen">是否偏心:"YES"/"NO"</param>
        /// <param name="Ecc_Dir">偏心方向</param>
        /// <param name="iData">i端偏心距</param>
        /// <param name="jData">j端偏心距</param>
        /// <param name="bJ_End">i端和j端偏心是否相同:"YES"/"NO"</param>
        public void updataEccenData(string bEccen, string Ecc_Dir, double iData,
            double jData, string bJ_End)
        {
            bool bb = false;
            if (bJ_End == "YES")
                bb = true;

            switch (bEccen)
            {
                case "NO":
                    beccen = false;
                    eccdir = DIR.GX;
                    i_end = 0;
                    j_end = 0;
                    bj_end = false;
                    return;
                case "YES":
                    beccen = true;
                    setEccenDir(Ecc_Dir);//设置偏心方向
                    i_end = iData;
                    j_end = jData;
                    bj_end = bb;
                    return;
                default:
                    beccen = false;
                    eccdir = DIR.GX;
                    i_end = 0;
                    j_end = 0;
                    bj_end = false;
                    return;
            }
        }

        /// <summary>
        /// 设置梁单元荷载信息数据
        /// </summary>
        /// <param name="dd1">位置1</param>
        /// <param name="pp1">荷载1</param>
        /// <param name="dd2">位置2</param>
        /// <param name="pp2">荷载2</param>
        /// <param name="dd3">位置3</param>
        /// <param name="pp3">荷载3</param>
        /// <param name="dd4">位置4</param>
        /// <param name="pp4">荷载4</param>
        public void setLoadData(double dd1, double pp1, double dd2, double pp2,
            double dd3, double pp3, double dd4, double pp4)
        {
            d1 = dd1;
            p1 = pp1;
            d2 = dd2;
            p2 = pp2;
            d3 = dd3;
            p3 = pp3;
            d4 = dd4;
            p4 = pp4;
        }

        /// <summary>
        /// 设置单元荷载方向
        /// </summary>
        /// <param name="direction">荷载方向字符串：GX/GY/GZ/LX/LY/LZ</param>
        public void setLoadDir(string direction)
        {
            direction = direction.ToUpper();
            switch (direction)
            {
                case "GX":
                    dir = DIR.GX;
                    return;
                case "GY":
                    dir = DIR.GY;
                    return;
                case "GZ":
                    dir = DIR.GZ;
                    return;
                case "LX":
                    dir = DIR.LX;
                    return;
                case "LY":
                    dir = DIR.LY;
                    return;
                case "LZ":
                    dir = DIR.LZ;
                    return;
                default:
                    dir = DIR.GX;
                    return;
            }
        }

        /// <summary>
        /// 设置偏心方向
        /// </summary>
        /// <param name="direction">偏心方向字符串：GX/GY/GZ/LX/LY/LZ</param>
        public void setEccenDir(string direction)
        {
            direction = direction.ToUpper();
            switch (direction)
            {
                case "GX":
                    eccdir = DIR.GX;
                    return;
                case "GY":
                    eccdir = DIR.GY;
                    return;
                case "GZ":
                    eccdir = DIR.GZ;
                    return;
                case "LX":
                    eccdir = DIR.LX;
                    return;
                case "LY":
                    eccdir = DIR.LY;
                    return;
                case "LZ":
                    eccdir = DIR.LZ;
                    return;
                default:
                    eccdir = DIR.GX;
                    return;
            }
        }

        /// <summary>
        /// 获取梁单元荷载数据的位置值
        /// </summary>
        /// <param name="i">位置编号：1,2,3,4</param>
        /// <returns>位置值：0~1之间的数值</returns>
        public double getD(int i)
        {
            double res = 0;
            switch (i)
            {
                case 1:
                    res = d1;
                    break;
                case 2:
                    res = d2;
                    break;
                case 3:
                    res = d3;
                    break;
                case 4:
                    res = d4;
                    break;
                default:
                    res = 0;
                    break;
            }
            return res;
        }

        /// <summary>
        /// 获取梁单元荷载数据的荷载值
        /// </summary>
        /// <param name="i">荷载值编号</param>
        /// <returns>荷载值</returns>
        public double getP(int i)
        {
            double res = 0;
            switch (i)
            {
                case 1:
                    res = p1;
                    break;
                case 2:
                    res = p2;
                    break;
                case 3:
                    res = p3;
                    break;
                case 4:
                    res = p4;
                    break;
                default:
                    res = 0;
                    break;
            }
            return res;
        }
    }
    #endregion

    #region Geometry Model Class(几何模型类)
    /// <summary>
    /// 定义存储文件信息的节点类：Bnodes
    /// </summary>
    public class Bnodes : Object
    {
        /// <summary>
        /// 节点编号
        /// </summary>
        public int num;
        /// <summary>
        /// 节点X坐标
        /// </summary>
        public double X;
        /// <summary>
        /// 节点Y坐标
        /// </summary>
        public double Y;
        /// <summary>
        /// 节点Z坐标
        /// </summary>
        public double Z;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="n">节点编号</param>
        public Bnodes(int n)
        {
            num = n;
            X = 0;
            Y = 0;
            Z = 0;
        }
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="n">节点编号</param>
        /// <param name="nx">节点X坐标</param>
        /// <param name="ny">节点Y坐标</param>
        /// <param name="nz">节点Z坐标</param>
        public Bnodes(int n, double nx, double ny, double nz)
        {
            num = n;
            X = nx;
            Y = ny;
            Z = nz;
        }

        //重载ToString函数
        new public string ToString()
        {
            return ("(" + num.ToString() + "," + X.ToString() + "," + Y.ToString() + "," + Z.ToString() + ")");
        }
    }

    /// <summary>
    /// 单元基类
    /// </summary>
    public abstract class Element : Object
    {
        private int _iEL, _iMAT, _iPRO;
        private string _TYPE;
        /// <summary>
        /// 单元编号
        /// </summary>
        public int iEL
        {
            get { return _iEL; }
            set { _iEL = value; }
        }
        /// <summary>
        /// 单元类型
        /// </summary>
        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }
        /// <summary>
        /// 单元材料号
        /// </summary>
        public int iMAT
        {
            get { return _iMAT; }
            set { _iMAT = value; }
        }
        /// <summary>
        /// 单元特性值号，即截面号
        /// </summary>
        public int iPRO
        {
            get { return _iPRO; }
            set { _iPRO = value; }
        }
        /// <summary>
        /// 单元节点号数组
        /// </summary>
        public List<int> iNs;

        /// <summary>
        ///构造函数 
        /// </summary>
        public Element()
        {
            iEL = 0;
            TYPE = "NOTYPE";
            iMAT = 0;
            iPRO = 0;
            iNs = new List<int>();
        }
        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="num">单元编号</param>
        /// <param name="type">单元类型</param>
        /// <param name="mat">单元材料号</param>
        /// <param name="pro">单元特性值号，或截面号</param>
        /// <param name="iNodes">节点号数组</param>
        public Element(int num, string type, int mat, int pro, params int[] iNodes)
        {
            iEL = num;
            TYPE = type;
            iMAT = mat;
            iPRO = pro;
            if (iNodes.Length < 2)
                return;
            else
            {
                iNs = new List<int>(iNodes.Length);
                foreach (int node in iNodes)
                {
                    iNs.Add(node);
                }
            }
        }
        //方法

        public string NodeString()
        {
            string temp = iEL.ToString();
            foreach (object num in iNs)
            {
                temp += ",";
                temp += num.ToString();
            }
            return temp;
        }
    }

    /// <summary>
    /// 梁单元类
    /// </summary>
    public class FrameElement : Element
    {
        /// <summary>
        /// 单元方向角（度）
        /// </summary>
        private double Angle;
        private int _iSUB;
        private double _EXVAL;

        /// <summary>
        /// 梁单元方向角（beta角）
        /// </summary>
        public double beta
        {
            get
            {
                return Angle;
            }
            set
            {
                Angle = value;
            }
        }

        /// <summary>
        /// 其它参数1
        /// </summary>
        public int iSUB
        {
            set { _iSUB = value; }
            get { return _iSUB; }
        }
        /// <summary>
        /// 其它参数2
        /// </summary>
        public double EXVAL
        {
            set { _EXVAL = value; }
            get { return _EXVAL; }
        }
        /// <summary>
        /// 其它参数3
        /// </summary>
        public int iOPT;

        /// <summary>
        /// 不带参数的构造函数,beta=0;type="BEAM"
        /// </summary>
        public FrameElement()
            : base()
        {
            this.TYPE = "BEAM";
            this.beta = 0;
        }
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="num">单元号</param>
        /// <param name="type">单元类型</param>
        /// <param name="mat">材料号</param>
        /// <param name="pro">截面特性号</param>
        /// <param name="iNodes">节点号数组</param>
        public FrameElement(int num, string type, int mat, int pro, params int[] iNodes)
            : base(num, type, mat, pro, iNodes)
        {
            //调用基类的构造函数
        }
    }

    /// <summary>
    /// 平面单元类
    /// </summary>
    public class PlanarElement : Element
    {
        private int _iSUB;
        private int _iWID;

        /// <summary>
        /// 厚度截面号1
        /// </summary>
        public int iSUB
        {
            get { return _iSUB; }
            set { _iSUB = value; }
        }

        /// <summary>
        /// 厚度截面号2
        /// </summary>
        public int iWID
        {
            get { return _iWID; }
            set { _iWID = value; }
        }

        /// <summary>
        /// 不带参数的构造函数
        /// </summary>
        public PlanarElement()
            : base()
        {
            this.TYPE = "PLATE";
        }

        /// <summary>
        /// 调用基类的构造函数
        /// </summary>
        /// <param name="num">单元号</param>
        /// <param name="type">单元类型</param>
        /// <param name="mat">材料号</param>
        /// <param name="pro">截面号，厚度号</param>
        /// <param name="iNodes">节点号数组</param>
        public PlanarElement(int num, string type, int mat, int pro, params int[] iNodes)
            : base(num, type, mat, pro, iNodes)
        {
            //调用基类的对应构造函数
        }
    }

    /// <summary>
    /// 存储截面特性的类
    /// </summary>
    public class BSections
    {
        /// <summary>
        /// 截面号
        /// </summary>
        private int iSEC;
        /// <summary>
        /// 截面类型：DBUSER或VALU
        /// </summary>
        public string TYPE;
        /// <summary>
        /// 截面名称
        /// </summary>
        private string SNAME;
        /// <summary>
        /// 截面偏移数据
        /// </summary>
        public ArrayList OFFSET;
        /// <summary>
        /// 截面偏心数据
        /// </summary>
        public bool bsd;
        /// <summary>
        /// 截面形状：B表示箱形
        /// </summary>
        public string SSHAPE;
        /// <summary>
        /// 截面数据信息
        /// </summary>
        public ArrayList SEC_Data;

        //属性
        /// <summary>
        /// 截面名称属性
        /// </summary>
        public string Name
        {
            get { return SNAME; }
            set { SNAME = value; }
        }
        /// <summary>
        /// 截面编号
        /// </summary>
        public int Num
        {
            get { return iSEC; }
            set { iSEC = value; }
        }

        public BSections()
        {
            iSEC = 1;
            TYPE = "DBUSER";
            SNAME = "No Name";

            OFFSET = new ArrayList(7);
            OFFSET.Add("CC");
            OFFSET.Add(0);
            OFFSET.Add(0);
            OFFSET.Add(0);
            OFFSET.Add(0);
            OFFSET.Add(0);
            OFFSET.Add(0);

            bsd = true;
            SSHAPE = "P";

            SEC_Data = new ArrayList();
            SEC_Data.Add(1);
            SEC_Data.Add("GB-YB");
            SEC_Data.Add("P 180x10");
        }
        /// <summary>
        /// 按一定格式输出截面数据信息
        /// </summary>
        /// <returns></returns>
        public string WriteData()
        {
            string res = null;
            if (this.SSHAPE.Trim() == "B" && (int)this.SEC_Data[0] == 2)//箱形截面
            {
                res += "sectype," + this.Num.ToString() + ",beam,hrec," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString() + "," + SEC_Data[3].ToString() + "," + SEC_Data[3].ToString()
                + "," + SEC_Data[6].ToString() + "," + SEC_Data[4].ToString();
            }
            else if (this.SSHAPE.Trim() == "H" && (int)this.SEC_Data[0] == 2)//H型截面
            {
                res += "sectype," + this.Num.ToString() + ",beam,i," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString() + "," + SEC_Data[4].ToString()
                    + "," + SEC_Data[4].ToString() + "," + SEC_Data[3].ToString();
            }
            else if (this.SSHAPE.Trim() == "P" && (int)this.SEC_Data[0] == 2)//圆管截面
            {
                double ri = (double)SEC_Data[1] / 2 - (double)SEC_Data[2];
                double ro = (double)SEC_Data[1] / 2;
                res += "sectype," + this.Num.ToString() + ",beam,ctube," + this.Name;
                res += "\nsecdata," + ri.ToString() + "," + ro.ToString();
            }
            else if (this.SSHAPE.Trim() == "SB" && (int)this.SEC_Data[0] == 2)//矩形截面
            {
                res += "sectype," + this.Num.ToString() + ",beam,rect," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString();
            }
            else if (this.SSHAPE.Trim() == "T" && (int)this.SEC_Data[0] == 2)//T型截面
            {
                res += "sectype," + this.Num.ToString() + ",beamr,t," + this.Name;
                res += "\nsecdata," + SEC_Data[1].ToString() + "," + SEC_Data[2].ToString() + "," + SEC_Data[3].ToString() + "," +
                    SEC_Data[4].ToString();
            }
            else if (this.SSHAPE.Trim() == "SR" && (int)this.SEC_Data[0] == 2)//圆形截面
            {
                res += "sectype," + this.Num.ToString() + ",beamr,csolid," + this.Name;
                res += "\nsecdata," + ((double)SEC_Data[1] / 2).ToString();
            }
            else
            {
                res += "!以下截面形状信息未处理：" + SSHAPE;
            }
            return res;
        }
    }

    /// <summary>
    /// 存储板单元厚度信息的类
    /// </summary>
    public class BThickness
    {
        private int _iTHK;
        private string _TYPE;
        private bool _bSAME;
        private double _THIK_IN, _THIK_OUT;

        /// <summary>
        /// 厚度截面号
        /// </summary>
        public int iTHK
        {
            set { _iTHK = value; }
            get { return _iTHK; }
        }

        /// <summary>
        /// 厚度截面类型
        /// </summary>
        public string TYPE
        {
            set { _TYPE = value; }
            get { return _TYPE; }
        }

        /// <summary>
        /// 是否平面内外同一数据
        /// </summary>
        public bool bSAME
        {
            set { _bSAME = value; }
            get { return _bSAME; }
        }

        /// <summary>
        /// 板单元面内厚度
        /// </summary>
        public double THIK_IN
        {
            set { _THIK_IN = value; }
            get { return _THIK_IN; }
        }

        /// <summary>
        /// 板单元面外厚度
        /// </summary>
        public double THIK_OUT
        {
            set { _THIK_OUT = value; }
            get { return _THIK_OUT; }
        }
    }
    #endregion

    #region Constraint (边界约束类)
    /// <summary>
    /// 边界条件类
    /// </summary>
    public class BConstraint : Object
    {
        /// <summary>
        /// 节点组
        /// </summary>
        public ArrayList node_list;
        private bool cUX;
        private bool cUY;
        private bool cUZ;
        private bool cRX;
        private bool cRY;
        private bool cRZ;
        //属性字段
        /// <summary>
        /// 是否约束UX
        /// </summary>
        public bool UX
        {
            get { return cUX; }
            set { cUX = value; }
        }
        /// <summary>
        /// 是否约束UY
        /// </summary>
        public bool UY
        {
            get { return cUY; }
            set { cUY = value; }
        }
        /// <summary>
        /// 是否约束UZ
        /// </summary>
        public bool UZ
        {
            get { return cUZ; }
            set { cUZ = value; }
        }
        /// <summary>
        /// 是否约束RX
        /// </summary>
        public bool RX
        {
            get { return cRX; }
            set { cRX = value; }
        }
        /// <summary>
        /// 是否约束RY
        /// </summary>
        public bool RY
        {
            get { return cRY; }
            set { cRY = value; }
        }
        /// <summary>
        /// 是否约束RZ
        /// </summary>
        public bool RZ
        {
            get { return cRZ; }
            set { cRZ = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BConstraint()
        {
            node_list = new ArrayList();
            UX = false;
            UY = false;
            UZ = false;
            RX = false;
            RY = false;
            RZ = false;
        }
    }
    #endregion

    /// <summary>
    /// 模型类：封装所有数据信息
    /// </summary>
    public class Bmodel : Object
    {
        #region 成员
        /// <summary>
        /// 单位系统
        /// </summary>
        public BUNIT unit;
        /// <summary>
        /// 节点信息列表
        /// </summary>
        public SortedList<int, Bnodes> nodes;
        /// <summary>
        /// 单元信息列表
        /// </summary>
        public SortedList<int, Element> elements;
        /// <summary>
        /// 截面信息列表
        /// </summary>
        public SortedList<int, BSections> sections;
        /// <summary>
        /// 板单元厚度表
        /// </summary>
        public SortedList<int, BThickness> thickness;

        /// <summary>
        /// 约束信息
        /// </summary>
        public List<BConstraint> constraint;

        /// <summary>
        /// 荷载工况列表
        /// </summary>
        public List<BLoadCase> STLDCASE;

        /// <summary>
        /// 节点荷载链表
        /// </summary>
        public SortedList<int, BNLoad> conloads;

        /// <summary>
        /// 梁单元荷载链表
        /// </summary>
        public SortedList<int, BBLoad> beamloads;

        /// <summary>
        /// 自重荷载信息链表
        /// </summary>
        public SortedList<string, BWeight> selfweight;
        #endregion

        /// <summary>
        /// 初始化模型数据
        /// </summary>
        public Bmodel()
        {
            unit = new BUNIT();

            nodes = new SortedList<int, Bnodes>();
            elements = new SortedList<int, Element>();
            sections = new SortedList<int, BSections>();
            thickness = new SortedList<int, BThickness>();

            constraint = new List<BConstraint>();

            STLDCASE = new List<BLoadCase>();
            conloads = new SortedList<int, BNLoad>(new RepeatedKeySort());//节点荷载
            beamloads = new SortedList<int, BBLoad>(new RepeatedKeySort());//梁单元荷载
            selfweight = new SortedList<string, BWeight>();//自重信息
        }
        /// <summary>
        /// 转化梁单元关键点信息
        /// </summary>
        public void GenBeamKpoint()
        {
            int Nnodes = 9999;//模型节点数基数，用于方向点的起始编号
            int i = 1;
            foreach (Element elee in this.elements.Values)
            {
                FrameElement ele = elee as FrameElement;
                if (ele != null && ele.beta != 0 && ele.iNs.Count < 3)
                {
                    Bnodes ndi = this.nodes[(int)ele.iNs[0]];//得到节点i
                    Bnodes ndj = this.nodes[(int)ele.iNs[1]];//得到节点j

                    if (ndi.X == ndj.X && ndi.Y == ndj.Y)
                    {
                        //如果单元坐标系x轴平行于全局坐标系z轴
                        Point3d pto = new Point3d(ndi.X + 10000, ndi.Y, ndi.Z);
                        Point3d pt1 = new Point3d(ndi.X, ndi.Y, ndi.Z);
                        Point3d pt2 = new Point3d(ndj.X, ndj.Y, ndj.Z);

                        Point3d ptk = RotNodebyAxis(pto, pt1, pt2, -ele.beta * Math.PI / 180);//求得方向点

                        Bnodes ndk = new Bnodes(Nnodes + i, ptk.X, ptk.Y, ptk.Z);
                        nodes.Add(Nnodes + i, ndk);//添加方向节点进模型数据库

                        ele.iNs.Add(Nnodes + i);// 添加方向节点号到单元数据
                    }
                    else
                    {
                        //如果单元坐标系x轴不平行于全局坐标系z轴
                        Point3d pto = new Point3d(ndi.X, ndi.Y, ndi.Z + 10000);
                        Point3d pt1 = new Point3d(ndi.X, ndi.Y, ndi.Z);
                        Point3d pt2 = new Point3d(ndj.X, ndj.Y, ndj.Z);

                        Point3d ptk = RotNodebyAxis(pto, pt1, pt2, -ele.beta * Math.PI / 180);//求得方向点

                        Bnodes ndk = new Bnodes(Nnodes + i, ptk.X, ptk.Y, ptk.Z);
                        nodes.Add(Nnodes + i, ndk);//添加方向节点进模型数据库

                        ele.iNs.Add(Nnodes + i);// 添加方向节点号到单元数据
                    }

                    i++;
                }
            }
        }
        /// <summary>
        /// 计算空间点绕任意轴旋转任意角度后的结果
        /// </summary>
        /// <param name="pt_original">原始节点</param>
        /// <param name="pt1_Axis">转轴起点</param>
        /// <param name="pt2_Axis">转轴终点</param>
        /// <param name="A">转角,以弧度计量</param>
        /// <returns>旋转后的节点###结果为转轴方向面向观察者，顺时针转去得到</returns>
        public static Point3d RotNodebyAxis(Point3d pt_original, Point3d pt1_Axis, Point3d pt2_Axis, double A)
        {
            //轴向量
            double x0 = pt2_Axis.X - pt1_Axis.X;
            double y0 = pt2_Axis.Y - pt1_Axis.Y;
            double z0 = pt2_Axis.Z - pt1_Axis.Z;
            Vector3 vf = Vector3.Normalize(new Vector3(x0, y0, z0));//单位化向量
            Vector3 vr = vf.CrossProduct(new Vector3(1, 0, 0));
            Vector3 vup = vf.CrossProduct(vr);

            RtwMatrix m = new RtwMatrix(3, 3);//
            m[0, 0] = (float)vr.X;
            m[1, 0] = (float)vr.Y;
            m[2, 0] = (float)vr.Z;

            m[0, 1] = (float)vup.X;
            m[1, 1] = (float)vup.Y;
            m[2, 1] = (float)vup.Z;

            m[0, 2] = (float)vf.X;
            m[1, 2] = (float)vf.Y;
            m[2, 2] = (float)vf.Z;

            RtwMatrix im = new RtwMatrix(3, 3);
            im = ~m;//矩阵转置

            RtwMatrix zrot = new RtwMatrix(3, 3);
            zrot[2, 2] = 1;
            zrot[0, 0] = (float)Math.Cos(A);
            zrot[0, 1] = (float)Math.Sin(A);
            zrot[1, 0] = (float)-Math.Sin(A);
            zrot[1, 1] = (float)Math.Cos(A);

            RtwMatrix M, mtemp = new RtwMatrix(3, 3);//变换矩阵
            mtemp = m * zrot;
            M = mtemp * im;

            //Tools.WriteMessage("\nM:\n"+M.ToString());
            Point3d pttemp = new Point3d(pt_original.X - pt1_Axis.X, pt_original.Y - pt1_Axis.Y, pt_original.Z - pt1_Axis.Z);

            double x1 = M[0, 0] * pttemp[0] + M[0, 1] * pttemp[1] + M[0, 2] * pttemp[2];
            double y1 = M[1, 0] * pttemp[0] + M[1, 1] * pttemp[1] + M[1, 2] * pttemp[2];
            double z1 = M[2, 0] * pttemp[0] + M[2, 1] * pttemp[1] + M[2, 2] * pttemp[2];

            x1 = x1 + pt1_Axis.X;
            y1 = y1 + pt1_Axis.Y;
            z1 = z1 + pt1_Axis.Z;
            Point3d pt_out = new Point3d(x1, y1, z1);

            //Tools.WriteMessage("计算点坐标"+pt_out.ToString());
            return pt_out;//反回坐标点
        }

        #region model类方法
        /// <summary>
        ///对模型数据进行标准化处理
        /// </summary>
        public void Normalize()
        {
            //截面类型标准化
            foreach (BSections sec in sections.Values)
            {
                //解决箱形截面当用户没有输入tf2时对截面数据进行标准化
                if (sec.SSHAPE.Trim() == "B" && (double)sec.SEC_Data[6] == 0
                    && (double)sec.SEC_Data[4] != 0)
                {
                    sec.SEC_Data[6] = sec.SEC_Data[4];
                }
                //todo:当输入截面为数据库截面时，进行截面参数转化
            }
        }

        /// <summary>
        /// 读取mgt文件信息
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        public void ReadFromMgt(string FilePath)
        {
            string currentdata = "notype";//指定当前数据类型
            string curLoadCase = "notype";//指定当前荷载工况
            //初始化模型信息数据
            //model = new Bmodel();
            //临时变量
            string[] temp, temp1 = null;
            int tempInt = 0;
            double tempDoublt1, tempDoublt2, tempDoublt3 = 0;
            int tempInt1, tempInt2, tempInt3, tempInt4, tempInt5, tempInt6, tempInt7 = 0;

            FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                //本行数据类型判断
                if (line.StartsWith(";") == true)
                {
                    //当前行为注释
                }
                else if (line.StartsWith("*USE-STLD") == true && line.Contains(","))
                {
                    if (line.Contains(";"))
                    {
                        line = line.Remove(line.IndexOf(';'));//去掉注释
                    }
                    curLoadCase = line;//当前荷载工况
                    currentdata = line;//得到当前数据内容
                }

                #region 自重荷载读取
                else if (line.StartsWith("*SELFWEIGHT") && curLoadCase != "notype")
                {
                    temp = line.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);
                    temp1 = curLoadCase.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);//荷载工况
                    try
                    {
                        BWeight weightdata = new BWeight();
                        weightdata.Gx = double.Parse(temp[1]);
                        weightdata.Gy = double.Parse(temp[2]);
                        weightdata.Gz = double.Parse(temp[3]);

                        weightdata.LC = temp1[1];//自重工况
                        string str_group = line.Substring(line.LastIndexOf(',') + 1);
                        if (str_group != " ")
                        {
                            weightdata.Group = str_group.Trim();//装入组名
                        }

                        selfweight.Add(weightdata.LC, weightdata);
                    }
                    catch
                    {
                        MessageBox.Show("解析自重信息出错！\n我晕，我再晕...", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion

                else if (line.StartsWith("*") == true)
                {
                    //如果命令行包含空格的话：
                    if (line.IndexOf(' ') > 0)
                    {
                        line = line.Remove(line.IndexOf(' '));
                    }
                    currentdata = line;//得到当前数据内容
                }
                #region 模型信息读取
                else if (line.StartsWith(" ") == true && currentdata == "*UNIT")
                {
                    line.Trim();//修剪开头空格
                    temp = line.Split(',');
                    try
                    {
                        unit.Force = temp[0].Trim().ToUpper();
                        unit.Length = temp[1].Trim().ToUpper();
                        unit.Heat = temp[2].Trim().ToUpper();
                        unit.Temper = temp[3].Trim().ToUpper();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
                #endregion
                #region 节点数据读取
                else if (line.StartsWith(" ") == true && currentdata == "*NODE")
                {
                    //进行节点数据读取
                    line.Trim();//修剪开头空格
                    temp = line.Split(',');
                    try
                    {
                        tempInt = int.Parse(temp[0], System.Globalization.NumberStyles.Number);
                        tempDoublt1 = double.Parse(temp[1], System.Globalization.NumberStyles.Float);
                        tempDoublt2 = double.Parse(temp[2], System.Globalization.NumberStyles.Float);
                        tempDoublt3 = double.Parse(temp[3], System.Globalization.NumberStyles.Float);
                        nodes.Add(tempInt, new Bnodes(tempInt, tempDoublt1, tempDoublt2
                            , tempDoublt3));
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                        //MessageBox.Show("解析节点数据字符串出错!");
                    }
                }
                #endregion
                #region 单元数据读取
                else if (line.StartsWith(" ") == true && currentdata == "*ELEMENT")
                {
                    //进行单元数据读取
                    temp = line.Split(',');
                    try
                    {
                        tempInt = int.Parse(temp[0], System.Globalization.NumberStyles.Number);
                        tempInt1 = int.Parse(temp[2], System.Globalization.NumberStyles.Number);
                        tempInt2 = int.Parse(temp[3], System.Globalization.NumberStyles.Number);
                        tempInt3 = int.Parse(temp[4], System.Globalization.NumberStyles.Number);
                        tempInt4 = int.Parse(temp[5], System.Globalization.NumberStyles.Number);


                        switch (temp[1].Trim().ToUpper())
                        {
                            case "BEAM":
                                tempDoublt1 = double.Parse(temp[6], System.Globalization.NumberStyles.Number);
                                FrameElement elemdata = new FrameElement(
                                    tempInt, "BEAM", tempInt1, tempInt2, tempInt3, tempInt4);
                                elemdata.beta = tempDoublt1;//记录单元方向角
                                elements.Add(tempInt, elemdata);
                                break;
                            case "PLATE":
                                tempInt5 = int.Parse(temp[6], System.Globalization.NumberStyles.Integer);
                                tempInt6 = int.Parse(temp[7], System.Globalization.NumberStyles.Integer);
                                tempInt7 = int.Parse(temp[8], System.Globalization.NumberStyles.Integer);
                                PlanarElement elemdata_P = new PlanarElement(
                                    tempInt, "PLATE", tempInt1, tempInt2, tempInt3, tempInt4, tempInt5, tempInt6);
                                elemdata_P.iSUB = tempInt7;
                                elements.Add(tempInt, elemdata_P);
                                break;
                            case "TENSTR":
                                tempDoublt2 = double.Parse(temp[6], System.Globalization.NumberStyles.Number);
                                tempInt5 = int.Parse(temp[7], System.Globalization.NumberStyles.Integer);
                                tempDoublt3 = double.Parse(temp[8], System.Globalization.NumberStyles.Integer);
                                FrameElement elemdata_T = new FrameElement(
                                    tempInt, "TENSTR", tempInt1, tempInt2, tempInt3, tempInt4);
                                elemdata_T.beta = tempDoublt2;//单元方向角
                                elemdata_T.iSUB = tempInt5;
                                elemdata_T.EXVAL = tempDoublt3;
                                elements.Add(tempInt, elemdata_T);
                                break;
                            default:
                                break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("解析单元信息出错!");
                    }
                }
                #endregion
                #region 截面数据读取
                else if (line.StartsWith(" ") == true && currentdata == "*SECTION")
                {
                    //进行截面属性读取
                    temp = line.Split(',');
                    try
                    {
                        tempInt = int.Parse(temp[0], System.Globalization.NumberStyles.Number);//截面编号

                        BSections secdata = new BSections();
                        secdata.Num = tempInt;
                        secdata.TYPE = temp[1];//截面类型
                        secdata.Name = temp[2];//截面名称
                        secdata.OFFSET[0] = temp[3];//截面偏心
                        for (int i = 1; i < 7; i++)
                            secdata.OFFSET[i] = double.Parse(temp[i + 3], System.Globalization.NumberStyles.Number);

                        secdata.SSHAPE = temp[11];//截面形状
                        secdata.SEC_Data.Clear();
                        secdata.SEC_Data.Add(int.Parse(temp[12], System.Globalization.NumberStyles.Number));//截面数据
                        if ((int)secdata.SEC_Data[0] == 2)
                        {
                            for (int j = 1; j < 11; j++)
                            {
                                secdata.SEC_Data.Add(double.Parse(temp[j + 12],
                                    System.Globalization.NumberStyles.Number));
                            }
                        }
                        else if ((int)secdata.SEC_Data[0] == 1)
                        {
                            secdata.SEC_Data.Add(temp[13]);
                            secdata.SEC_Data.Add(temp[14]);
                        }

                        sections.Add(tempInt, secdata);//输出变量
                    }
                    catch
                    {
                        MessageBox.Show("解析截面属性出错！\n是否选用了不支持的截面数据类型？？", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
                #region 板单元厚度数据读取
                else if (line.StartsWith(" ") == true && currentdata == "*THICKNESS")
                {
                    temp = line.Split(',');
                    try
                    {
                        tempInt = int.Parse(temp[0], System.Globalization.NumberStyles.Number);//厚度编号
                        BThickness thidata = new BThickness();
                        thidata.iTHK = tempInt;
                        thidata.TYPE = temp[1].Trim();
                        thidata.bSAME = true;
                        if (temp[2].Trim() == "NO")
                            thidata.bSAME = false;
                        thidata.THIK_IN = double.Parse(temp[3].Trim(), System.Globalization.NumberStyles.Float);
                        thidata.THIK_OUT = double.Parse(temp[4].Trim(), System.Globalization.NumberStyles.Float);

                        thickness.Add(tempInt, thidata);//输出变量
                    }
                    catch
                    {
                        MessageBox.Show("解析截面厚度出错！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
                #region 边界条件数据读取
                else if (line.StartsWith(" ") && currentdata == "*CONSTRAINT")
                {
                    //进行边界条件读取
                    BConstraint support = new BConstraint();
                    temp = line.Split(',');
                    if (temp[0].Trim().Contains(" "))
                    {
                        temp1 = temp[0].Trim().Split(' ');//节点列表
                        support.node_list.AddRange(temp1);
                    }
                    else
                    {
                        support.node_list.Add(temp[0]);
                    }

                    //读取约束情况
                    for (int i = 0; i < temp[1].Trim().Length; i++)
                    {
                        if (temp[1].Trim()[i] == '1')
                        {
                            switch (i)
                            {
                                case 0:
                                    support.UX = true;
                                    break;
                                case 1:
                                    support.UY = true;
                                    break;
                                case 2:
                                    support.UZ = true;
                                    break;
                                case 3:
                                    support.RX = true;
                                    break;
                                case 4:
                                    support.RY = true;
                                    break;
                                case 5:
                                    support.RZ = true;
                                    break;
                            }
                        }
                    }

                    constraint.Add(support);//添加到模型数据库中

                }
                #endregion
                #region 工况列表数据读取
                else if (line.StartsWith(" ") == true && currentdata == "*STLDCASE")
                {
                    //拆分字符串
                    temp = line.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        BLoadCase lcdata = new BLoadCase();
                        lcdata.LCName = temp[0];

                        switch (temp[1])
                        {
                            case "D":
                                lcdata.LCType = LCType.D;
                                break;
                            case "L":
                                lcdata.LCType = LCType.L;
                                break;
                            case "W":
                                lcdata.LCType = LCType.W;
                                break;
                            case "E":
                                lcdata.LCType = LCType.E;
                                break;
                            case "LR":
                                lcdata.LCType = LCType.LR;
                                break;
                            case "S":
                                lcdata.LCType = LCType.S;
                                break;
                            case "T":
                                lcdata.LCType = LCType.T;
                                break;
                            case "PS":
                                lcdata.LCType = LCType.PS;
                                break;
                            default:
                                lcdata.LCType = LCType.USER;
                                break;
                        }

                        STLDCASE.Add(lcdata);//添加到模型数据库
                    }
                    catch
                    {
                        MessageBox.Show("解析工况列表出错！\n我晕，我狂晕...", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion

                #region 节点荷载列表读取
                else if (line.StartsWith(" ") == true && currentdata == "*CONLOAD")
                {
                    //拆分字符
                    temp = curLoadCase.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);//temp[1]为工况名
                    temp1 = line.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);//temp1为数据组

                    try
                    {
                        BNLoad BNLoaddata = new BNLoad(int.Parse(temp1[0]));
                        BNLoaddata.FX = double.Parse(temp1[1]);
                        BNLoaddata.FY = double.Parse(temp1[2]);
                        BNLoaddata.FZ = double.Parse(temp1[3]);
                        BNLoaddata.MX = double.Parse(temp1[4]);
                        BNLoaddata.MY = double.Parse(temp1[5]);
                        BNLoaddata.MZ = double.Parse(temp1[6]);

                        BNLoaddata.LC = temp[1];//工况名称
                        string str_group = line.Substring(line.LastIndexOf(',') + 1);
                        if (str_group != " ")
                        {
                            BNLoaddata.Group = str_group.Trim();//装入组名
                        }

                        conloads.Add(BNLoaddata.iNode, BNLoaddata);//加入模型数据库
                    }
                    catch
                    {
                        MessageBox.Show("解析节点荷载列表出错！\n我晕，我狂晕...", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
                #region 梁单元荷载列表读取
                else if (line.StartsWith(" ") == true && currentdata == "*BEAMLOAD")
                {
                    //拆分字符
                    temp = curLoadCase.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);//temp[1]为工况名
                    temp1 = line.Split(new char[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries);//temp1为数据组
                    try
                    {
                        BBLoad BeamLoadData = new BBLoad();
                        BeamLoadData.ELEM_num = int.Parse(temp1[0].Trim());
                        BeamLoadData.CMD = temp1[1].Trim();
                        BeamLoadData.TYPE = temp1[2].Trim();
                        BeamLoadData.setLoadDir(temp1[3].Trim());
                        if (temp1[4].Trim() == "YES")
                            BeamLoadData.bPROJ = true;

                        //更新偏心数据
                        BeamLoadData.readEccenDataMgt(line);

                        double dd1 = double.Parse(temp1[10].Trim());
                        double pp1 = double.Parse(temp1[11].Trim());
                        double dd2 = double.Parse(temp1[12].Trim());
                        double pp2 = double.Parse(temp1[13].Trim());
                        double dd3 = double.Parse(temp1[14].Trim());
                        double pp3 = double.Parse(temp1[15].Trim());
                        double dd4 = double.Parse(temp1[16].Trim());
                        double pp4 = double.Parse(temp1[17].Trim());
                        BeamLoadData.setLoadData(dd1, pp1, dd2, pp2, dd3, pp3, dd4, pp4);//更新荷载数据

                        BeamLoadData.LC = temp[1];//工况
                        if (temp1[18] != " ")
                        {
                            BeamLoadData.Group = temp1[18].Trim();//装入组名
                        }

                        beamloads.Add(BeamLoadData.ELEM_num, BeamLoadData);//加入模型数据库
                    }
                    catch
                    {
                        MessageBox.Show("解析梁单元荷载列表出错！\n我晕，我狂晕...", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
            }
            reader.Close();

            Normalize();//模型标准化处理
            GenBeamKpoint();//计算模型中梁单元的节点方向点信息
        }

        /// <summary>
        /// 写出ANSYS命令流文件
        /// </summary>
        /// <param name="inp">写入文件路径</param>
        /// <param name="BeamType">梁单元类型：1表示beam44，2表示beam188，3表示beam189</param>
        public bool WriteToInp(string inp, int BeamType)
        {
            FileStream stream = File.Open(inp, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("FINISH");
            writer.WriteLine("/CLEAR");
            writer.WriteLine("/COM,Midas2Ansys INP File Created at " + System.DateTime.Now);
            writer.WriteLine("/COM,*******http://www.lubanren.com********");

            writer.WriteLine("\n/PREP7");
            writer.WriteLine("\n!单元类型信息...");
            switch (BeamType)//根据选择输出单元类型定义命令
            {
                case 1:
                    writer.WriteLine("et,1,44");
                    writer.WriteLine("keyopt,1,10,1");
                    break;
                case 2:
                    writer.WriteLine("et,1,188");
                    writer.WriteLine("keyopt,1,3,2");
                    writer.WriteLine("keyopt,1,8,2");
                    writer.WriteLine("keyopt,1,9,2");
                    break;
                case 3:
                    writer.WriteLine("et,1,189");
                    break;
                default:
                    writer.WriteLine("et,1,188");
                    break;
            }
            //板单元类型声明
            writer.WriteLine("et,2,43");

            //实常数信息
            foreach (KeyValuePair<int, BThickness> thi in this.thickness)
            {
                writer.WriteLine("r,{0},{1}", thi.Key.ToString(), thi.Value.THIK_IN.ToString());
            }

            writer.WriteLine("\n!截面信息定义...");
            foreach (KeyValuePair<int, BSections> sec in this.sections)
            {
                writer.WriteLine(sec.Value.WriteData());
            }

            writer.WriteLine("\n!材料信息定义...");
            writer.WriteLine("\n!节点数据信息");
            //输出节点信息
            foreach (KeyValuePair<int, Bnodes> node in this.nodes)
            {
                writer.WriteLine("n," + node.Key.ToString("0") + "," + node.Value.X.ToString() + "," + node.Value.Y.ToString() + "," + node.Value.Z
                    .ToString());
            }
            //输出单元信息
            writer.WriteLine("\n!单元数据信息");
            foreach (KeyValuePair<int, Element> elem in this.elements)
            {
                //按单元类型分类输出
                switch (elem.Value.TYPE)
                {
                    case "BEAM":
                        writer.WriteLine("type,1");
                        writer.WriteLine("secnum," + elem.Value.iPRO.ToString());
                        writer.WriteLine("en," + elem.Value.NodeString());
                        break;
                    case "PLATE":
                        //writer.WriteLine("!{0}号单元是平面单元", elem.Value.iEL.ToString());
                        writer.WriteLine("real,{0}", elem.Value.iPRO.ToString());//厚度号
                        writer.WriteLine("type,2");
                        writer.WriteLine("en," + elem.Value.NodeString());
                        break;
                    case "TENSTR":
                        writer.WriteLine("!{0}号单元是只拉单元", elem.Value.iEL.ToString());
                        break;
                    default:
                        break;
                }

            }
            //进入后处理模块
            writer.WriteLine("\n/SOLU");
            writer.WriteLine("\n!约束条件");
            #region 边界条件输出
            foreach (BConstraint nodesuport in this.constraint)
            {
                foreach (string nodeslist in nodesuport.node_list)
                {
                    //apdl命令格式如下：
                    //D, NODE, Lab, VALUE, VALUE2, NEND, NINC, Lab2, Lab3, Lab4, Lab5, Lab6
                    string NODE = "", Lab = "", NEND = "", NINC = "", Lab2 = "", Lab3 = "", Lab4 = "", Lab5 = "", Lab6 = "";
                    if (nodeslist.Contains("by") && nodeslist.Contains("to"))
                    {
                        NODE = nodeslist.Remove(nodeslist.IndexOf("to"));
                        NEND = nodeslist.Substring(nodeslist.IndexOf("to") + 2, nodeslist.IndexOf("by") - nodeslist.IndexOf("to") - 2);
                        NINC = nodeslist.Substring(nodeslist.IndexOf("by") + 2);

                    }
                    else if (nodeslist.Contains("to"))
                    {
                        NODE = nodeslist.Remove(nodeslist.IndexOf("to"));
                        NEND = nodeslist.Substring(nodeslist.IndexOf("to") + 2);
                    }
                    else
                    {
                        NODE = nodeslist;
                    }


                    if (nodesuport.UX == true)
                        Lab = "ux";
                    if (nodesuport.UY == true)
                        Lab2 = "uy";
                    if (nodesuport.UZ == true)
                        Lab3 = "uz";
                    if (nodesuport.RX == true)
                        Lab4 = "rotx";
                    if (nodesuport.RY == true)
                        Lab5 = "roty";
                    if (nodesuport.RZ == true)
                        Lab6 = "rotz";

                    writer.WriteLine("d," + NODE + "," + Lab + ",,," + NEND + "," + NINC + "," + Lab2 + "," + Lab3 + "," + Lab4 + "," + Lab5
                        + "," + Lab6);
                }
            }
            #endregion
            #region 荷载输出
            writer.WriteLine("\n!施加模型荷载");
            foreach (BLoadCase lc in this.STLDCASE)
            {
                writer.WriteLine("\n!工况{0}", lc.LCName);
                writer.WriteLine("*create,LC_{0},lc", lc.LCName);
                #region 输出自重荷载
                foreach (KeyValuePair<string, BWeight> weightdata in this.selfweight)
                {
                    if (weightdata.Value.LC == lc.LCName)
                    {
                        //注意ANSYS中加速度方向反号
                        writer.WriteLine("acel,{0},{1},{2}",
                            (-weightdata.Value.ACELx).ToString(),
                            (-weightdata.Value.ACELy).ToString(),
                            (-weightdata.Value.ACELz).ToString());
                    }
                }
                #endregion
                #region 输出节点荷载
                foreach (KeyValuePair<int, BNLoad> nload in this.conloads)
                {
                    if (nload.Value.LC == lc.LCName)
                    {
                        if (nload.Value.FX != 0)
                        {
                            writer.WriteLine("f,{0},fx,{1}", nload.Value.iNode.ToString(),
                                nload.Value.FX.ToString());
                        }
                        if (nload.Value.FY != 0)
                        {
                            writer.WriteLine("f,{0},fy,{1}", nload.Value.iNode.ToString(),
                                nload.Value.FY.ToString());
                        }
                        if (nload.Value.FZ != 0)
                        {
                            writer.WriteLine("f,{0},fz,{1}", nload.Value.iNode.ToString(),
                                nload.Value.FZ.ToString());
                        }
                        if (nload.Value.MX != 0)
                        {
                            writer.WriteLine("f,{0},mx,{1}", nload.Value.iNode.ToString(),
                                nload.Value.MX.ToString());
                        }
                        if (nload.Value.MY != 0)
                        {
                            writer.WriteLine("f,{0},my,{1}", nload.Value.iNode.ToString(),
                                nload.Value.MY.ToString());
                        }
                        if (nload.Value.MZ != 0)
                        {
                            writer.WriteLine("f,{0},mz,{1}", nload.Value.iNode.ToString(),
                                nload.Value.MZ.ToString());
                        }
                    }
                }
                #endregion
                #region 输出梁单元荷载
                foreach (KeyValuePair<int, BBLoad> bload in this.beamloads)
                {
                    if (bload.Value.LC == lc.LCName)
                    {
                        //to do:输出单元荷载信息
                        if (bload.Value.TYPE == "UNIMOMENT" ||
                            bload.Value.TYPE == "CONMOMENT" ||
                            bload.Value.getP(3) != 0)
                        {
                            writer.WriteLine("!单元({0})在ANSYS中需要单元细化...", bload.Key.ToString());
                        }
                        else if (bload.Value.TYPE == "UNILOAD")
                        {
                            switch (bload.Value.Dir)
                            {
                                case DIR.GX:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系不是局部坐标",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GY:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系不是局部坐标",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GZ:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系不是局部坐标",
                                        bload.Key.ToString());
                                    break;
                                case DIR.LZ:
                                    writer.WriteLine("sfbeam,{0},1,pres,{1},{2},,,{3},{4}",
                                        bload.Key.ToString(),
                                        (-bload.Value.getP(1)).ToString(),
                                        (-bload.Value.getP(2)).ToString(),
                                        bload.Value.getD(1).ToString(),
                                        bload.Value.getD(2).ToString());
                                    break;
                                case DIR.LY:
                                    writer.WriteLine("sfbeam,{0},2,pres,{1},{2},,,{3},{4}",
                                        bload.Key.ToString(),
                                        (-bload.Value.getP(1)).ToString(),
                                        (-bload.Value.getP(2)).ToString(),
                                        bload.Value.getD(1).ToString(),
                                        bload.Value.getD(2).ToString());
                                    break;
                                case DIR.LX:
                                    writer.WriteLine("sfbeam,{0},3,pres,{1},{2},,,{3},{4}",
                                        bload.Key.ToString(),
                                        bload.Value.getP(1).ToString(),
                                        bload.Value.getP(2).ToString(),
                                        bload.Value.getD(1).ToString(),
                                        bload.Value.getD(2).ToString());
                                    break;
                                default:
                                    break;
                            }

                        }
                        else if (bload.Value.TYPE == "CONLOAD" && bload.Value.getP(2) == 0)
                        {
                            switch (bload.Value.Dir)
                            {
                                case DIR.GX:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系不是局部坐标",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GY:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系不是局部坐标",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GZ:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系不是局部坐标",
                                        bload.Key.ToString());
                                    break;
                                case DIR.LZ:
                                    writer.WriteLine("sfbeam,{0},1,pres,{1},,,,{3},-1",
                                        bload.Key.ToString(),
                                        (-bload.Value.getP(1)).ToString(),
                                        bload.Value.getD(1).ToString(),
                                        bload.Value.getD(2).ToString());
                                    break;
                                case DIR.LY:
                                    writer.WriteLine("sfbeam,{0},2,pres,{1},,,,{3},-1",
                                        bload.Key.ToString(),
                                        (-bload.Value.getP(1)).ToString(),
                                        bload.Value.getD(1).ToString(),
                                        bload.Value.getD(2).ToString());
                                    break;
                                case DIR.LX:
                                    writer.WriteLine("sfbeam,{0},3,pres,{1},,,,{3},-1",
                                        bload.Key.ToString(),
                                        bload.Value.getP(1).ToString(),
                                        bload.Value.getD(1).ToString(),
                                        bload.Value.getD(2).ToString());
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                #endregion
                writer.WriteLine("*end");
            }
            #endregion
            writer.WriteLine("\nallsel,all");
            writer.WriteLine("eplot");
            writer.Close();
            stream.Close();
            return true;
        }
        #endregion
    }

    /// <summary>
    /// 点类
    /// </summary>
    public class Point3d : Object
    {
        private double XX, YY, ZZ;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Point3d()
        {
            XX = 0;
            YY = 0;
            ZZ = 0;
        }
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="nx">节点X坐标</param>
        /// <param name="ny">节点Y坐标</param>
        /// <param name="nz">节点Z坐标</param>
        public Point3d(double nx, double ny, double nz)
        {
            XX = nx;
            YY = ny;
            ZZ = nz;
        }
        //属性
        public double X
        {
            get { return XX; }
            set { XX = value; }
        }
        public double Y
        {
            get { return YY; }
            set { YY = value; }
        }
        public double Z
        {
            get { return ZZ; }
            set { ZZ = value; }
        }
        //索引函数
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: { return XX; }
                    case 1: { return YY; }
                    case 2: { return ZZ; }
                    default: throw new ArgumentException(THREE_COMPONENTS, "index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: { XX = value; break; }
                    case 1: { YY = value; break; }
                    case 2: { ZZ = value; break; }
                    default: throw new ArgumentException(THREE_COMPONENTS, "index");
                }
            }
        }

        private const string THREE_COMPONENTS =
            "Point3d must contain exactly three components,(x,y,z)";
    }

    /// <summary>
    /// 实现hash表重复键成员的添加
    /// </summary>
    public class RepeatedKeySort : IComparer<int>
    {
        #region IComparer 成员
        public int Compare(int x, int y)
        {
            //return -1;//直接返回不进行排序
            //以下代码可实现自动排序
            int iResult = x - y;
            if (iResult == 0) iResult = -1;
            return iResult;
        }
        #endregion
    }
}
