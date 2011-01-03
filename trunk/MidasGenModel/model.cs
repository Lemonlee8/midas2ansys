using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MidasGenModel.Design;
using MidasGenModel.Geometry3d;


namespace MidasGenModel.model
{
    #region Model Info(模型特性类)

    /// <summary>
    /// 模型单位信息
    /// </summary>
    [Serializable]
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
    /// 荷载组合的种类
    /// </summary>
    public enum LCKind
    {
        /// <summary>
        /// General组合
        /// </summary>
        GEN,
        /// <summary>
        /// 钢结构设计用组合
        /// </summary>
        STEEL,
        /// <summary>
        /// 混凝土设计用组合
        /// </summary>
        CONC,
        /// <summary>
        /// SRC设计用组合
        /// </summary>
        SRC,
        /// <summary>
        /// 基础设计用组合
        /// </summary>
        FDN
    }

    /// <summary>
    /// 单位荷载条件种类
    /// </summary>
    public enum ANAL
    {
        /// <summary>
        /// Static 静力
        /// </summary>
        ST,
        /// <summary>
        /// Response Spectrum 反应谱
        /// </summary>
        RS,
        /// <summary>
        /// 偶然偏心的反应谱结果
        /// </summary>
        ES,
        /// <summary>
        /// Time History 时程
        /// </summary>
        TH,
        /// <summary>
        /// Moving 移动
        /// </summary>
        MV,
        /// <summary>
        /// Settlement 沉降
        /// </summary>
        SM,
        /// <summary>
        /// 组合
        /// </summary>
        CB,
        /// <summary>
        /// 钢结构组合
        /// </summary>
        CBS
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
    [Serializable]
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
    /// 荷载工况组和系数对
    /// </summary>
    [Serializable]
    public class BLCFactGroup
    {
        /// <summary>
        /// 单位荷载条件的种类
        /// </summary>
        public ANAL ANAL;
        /// <summary>
        /// 工况名称
        /// </summary>
        public string  LCNAME;
        /// <summary>
        /// 单位荷载条件的荷载系数
        /// </summary>
        public double FACT;
    }
    /// <summary>
    /// 荷载组合类
    /// </summary>
    [Serializable]
    public class BLoadComb
    {
        protected string _NAME;//荷载组合条件的名称
        protected LCKind _KIND;//荷载组合的种类
        protected bool _bACTIVE;//是否激活
        protected bool _bES;//不清楚的参数：一般多为NO
        protected int _iTYPE;//指定荷载组合方式：0为线性，1为+SRSS,2为-SRSS，3为平方开根号
        protected string _DESC;//简单说明
        protected List<BLCFactGroup> _LoadCombData;//荷载组合数据,一般为mgt文件第二行后数据

        /// <summary>
        /// 荷载组合条件的名称
        /// </summary>
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        /// <summary>
        /// 荷载组合的种类
        /// </summary>
        public LCKind KIND
        {
            get { return _KIND; }
        }
        /// <summary>
        /// 荷载组合描述
        /// </summary>
        public string DESC
        {
            get { return _DESC; }
            set {_DESC=value;}
        }
        /// <summary>
        /// 当前组合是否激活
        /// </summary>
        public bool bACTIVE
        {
            get { return _bACTIVE; }
            set { _bACTIVE = value; }
        }
        /// <summary>
        /// 指定荷载组合方式：0为线性，1为+SRSS,2为-SRSS，3为平方开根号
        /// </summary>
        public int iTYPE
        {
            get { return _iTYPE; }
        }

        /// <summary>
        /// 取得工况组的数量
        /// </summary>
        public int Num_LCGroup
        {
            get { return _LoadCombData.Count; }
        }

        /// <summary>
        /// 荷载工况系数对数据
        /// </summary>
        public List<BLCFactGroup> LoadCombData
        {
            get { return _LoadCombData; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BLoadComb()
        {
            _LoadCombData = new List<BLCFactGroup>();
        }

        /// <summary>
        /// 设置组合基本信息
        /// </summary>
        /// <param name="Name">组合条件名称</param>
        /// <param name="Kind">荷载组合种类</param>
        /// <param name="bActive">是否激活</param>
        /// <param name="bEs">不清楚的参数：一般多为NO</param>
        /// <param name="iType">指定荷载组合方式：0为线性，1为+SRSS,2为-SRSS</param>
        /// <param name="Desc">简单说明</param>
        public void SetData1(string Name, LCKind Kind, bool bActive, bool bEs,
            int iType, string Desc)
        {
            _NAME = Name;
            _KIND = Kind;
            _bACTIVE = bActive;
            _bES = bEs;
            _iTYPE = iType;
            _DESC = Desc;
        }

        /// <summary>
        /// 添加荷载工况组和系数对入当前组合
        /// </summary>
        /// <param name="lcfg">荷载工况组和系数对</param>
        public void AddLCFactGroup(BLCFactGroup lcfg)
        {
            _LoadCombData.Add(lcfg);
        }

        /// <summary>
        /// 荷载组合初始化：移除所有组合数据
        /// </summary>
        public void Clear()
        {
            _NAME="";
            _KIND=LCKind.GEN;
            _bACTIVE=true;
            _bES=false;
            _iTYPE=0;
            _DESC="";
            _LoadCombData.Clear();//移除所有元素
        }

        /// <summary>
        /// 判断当前组合是否含有某个工况
        /// </summary>
        /// <param name="LCName">工况名</param>
        /// <returns>含有为true</returns>
        public bool hasLC(string LCName)
        {
            foreach (BLCFactGroup bfg in _LoadCombData)
            {
                if (bfg.LCNAME == LCName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断当年组合是否含有某种类型工况，如地震反应谱
        /// </summary>
        /// <param name="type">工况类型</param>
        /// <returns>是或否</returns>
        public bool hasLC_ANAL(ANAL type)
        {
            foreach (BLCFactGroup bfg in _LoadCombData)
            {
                if (bfg.ANAL==type)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 荷载组合表
    /// </summary>
    [Serializable]
    public class BLoadCombTable
    {
        #region 数据成员
        private List<string> _ComGen;//一般组合表
        private List<string> _ComSteel;//钢结构验算用表
        private List<string> _ComCon;//混凝土验算用表
        private Hashtable _LoadCombData;//所有模型中组合数据哈希表
        #endregion
        #region 属性
        /// <summary>
        /// 一般组合表
        /// </summary>
        public List<string> ComGen
        {
            get { return _ComGen; }
        }
        /// <summary>
        /// 钢结构验算用表
        /// </summary>
        public List<string> ComSteel
        {
            get { return _ComSteel; }
        }
        /// <summary>
        /// 混凝土验算用表
        /// </summary>
        public List<string> ComCon
        {
            get { return _ComCon; }
        }
        /// <summary>
        /// 模型中所有组合数据哈希表
        /// </summary>
        public Hashtable LoadCombData
        {
            get { return _LoadCombData; }
        }
        #endregion
        #region 构造函数
        public BLoadCombTable()
        {
            _ComGen = new List<string>();
            _ComSteel = new List<string>();
            _ComCon = new List<string>();
            _LoadCombData = new Hashtable();
        }
        #endregion
        #region 方法
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>荷载组合</returns>
        public BLoadComb this[string key]
        {
            get
            {
                return _LoadCombData[key] as BLoadComb;
            }
        }
        /// <summary>
        /// 添加荷载组合数据入表
        /// </summary>
        /// <param name="com"></param>
        public void  Add(BLoadComb com)
        {
            //记录原始组合顺序
            switch (com.KIND)
            {
                case LCKind.GEN: _ComGen.Add(com.NAME); break;
                case LCKind.STEEL: _ComSteel.Add(com.NAME); break;
                case LCKind.CONC: _ComCon.Add(com.NAME); break;
                default: _ComGen.Add(com.NAME); break;
            }
            _LoadCombData.Add(com.NAME, com);//添加数据
        }
        /// <summary>
        /// 移除指定荷载组合
        /// </summary>
        /// <param name="comName"></param>
        public void Remove(string comName)
        {
            //如果不包含此组合
            if (!_LoadCombData.ContainsKey(comName))
                return;
            else
            {
                BLoadComb com = _LoadCombData[comName] as BLoadComb;
                switch (com.KIND)
                {
                    case LCKind.GEN: _ComGen.Remove(comName); break;
                    case LCKind.STEEL: _ComSteel.Remove(comName); break;
                    case LCKind.CONC: _ComCon.Remove(comName); break;
                    default: _ComGen.Remove(comName); break;
                }
                _LoadCombData.Remove(comName);//移除数据
            }
        }
        /// <summary>
        /// 查找组合表是否含有某个组合
        /// </summary>
        /// <param name="Key">组合关键字</param>
        /// <returns>是或否</returns>
        public bool ContainsKey(string Key)
        {
            return _LoadCombData.ContainsKey(Key);
        }
        #endregion
    }
    /// <summary>
    /// 荷载类
    /// </summary>
    [Serializable]
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
    [Serializable]
    public class BNLoad : Load
    {
        private int node;//节点号
        private double fx, fy, fz, mx, my, mz;
        #region 属性
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
        #endregion

        #region 构造函数
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
        #endregion

        #region 方法
        /// <summary>
        /// 对节点荷载进行比例放大
        /// </summary>
        /// <param name="factor">系数</param>
        public void Magnify(double factor)
        {
            fx = fx * factor;
            fy = fy * factor;
            fz = fz * factor;
            mx = mx * factor;
            my = my * factor;
            mz = mz * factor;
        }
        #endregion
    }

    /// <summary>
    /// 自重荷载类
    /// </summary>
    [Serializable]
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
    /// 梁单元荷载类型
    /// </summary>
    public enum BeamLoadType
    {
        /// <summary>
        /// Uniform Loads 均布荷载
        /// </summary>
        UNILOAD,
        /// <summary>
        /// Concentrated Forces 集中力
        /// </summary>
        CONLOAD,
        /// <summary>
        /// Concentrated Moments 集中弯矩
        /// </summary>
        CONMOMENT,
        /// <summary>
        /// Uniform Moments/Torsions 均布弯矩或扭矩
        /// </summary>
        UNIMOMENT
    }

    /// <summary>
    /// 梁单元荷载类
    /// </summary>
    [Serializable]
    public class BBLoad : Load
    {
        private int elem_num;//单元号
        private string cmd;
        private BeamLoadType _type;
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
        public BeamLoadType TYPE
        {
            get { return _type; }
            set { _type = value; }
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
            _type = BeamLoadType.UNILOAD;
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

    /// <summary>
    /// 单元温度荷载类
    /// </summary>
    [Serializable]
    public class BETLoad : Load
    {
        private int _elem_num;//单元号
        private double _Temp;//单元温度

        #region 属性
        /// <summary>
        /// 单元号
        /// </summary>
        public int Elem_Num
        {
            get { return _elem_num;}
            set { _elem_num = value; }
        }

        /// <summary>
        /// 温度荷载
        /// </summary>
        public double Temp
        {
            get { return _Temp; }
            set { _Temp = value; }
        }
        /// <summary>
        /// 荷载分组
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
        /// 荷载工况
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
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public BETLoad()
        {
            _elem_num = 0;
            _Temp = 0;
        }
        #endregion
    }

    /// <summary>
    /// 节点荷载表类
    /// </summary>
    [Serializable]
    public class BLoadTable
    {
        #region 数据成员
        private Hashtable _NLoadData;//节点荷载数据
        private Hashtable _BeamLoadData;//单元荷载数据
        #endregion

        #region 属性
        /// <summary>
        /// 节点荷载数据
        /// </summary>
        public Hashtable NLoadData
        {
            get { return _NLoadData; }
        }

        /// <summary>
        /// 具有节点荷载的节点号列表
        /// </summary>
        public List<int> NodeListForNLoad
        {
            get
            {
                List<int> Res = new List<int>();
                foreach (DictionaryEntry de in _NLoadData)
                {
                    SortedList<int, BNLoad> ND = de.Value as SortedList<int, BNLoad>;
                    foreach (KeyValuePair<int, BNLoad> kvp in ND)
                    {
                        if (Res.Contains(kvp.Key)==false)
                            Res.Add(kvp.Key);
                    }
                }

                return Res;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数初始化
        /// </summary>
        public BLoadTable()
        {
            _NLoadData = new Hashtable();
            _BeamLoadData = new Hashtable();
        }
        #endregion
        #region 方法
        /// <summary>
        /// 按旧数据更新荷载表中的节点荷载
        /// </summary>
        /// <param name="LCs">工况列表</param>
        /// <param name="NLoadData">旧版节点荷载数据</param>
        public void  UpdateNodeLoadList(List<BLoadCase> LCs,SortedList<int,BNLoad> NLoadData)
        {
            foreach (BLoadCase lc in LCs)
            {
                //当前组合的节点荷载表
                SortedList<int, BNLoad> CurNLoadData = new SortedList<int,BNLoad> ();

                foreach (KeyValuePair<int, BNLoad> NLoad in NLoadData)
                {
                    if (NLoad.Value.LC == lc.LCName)
                    {
                        CurNLoadData.Add(NLoad.Key, NLoad.Value);
                    }
                }
                //添加当前组合节点表入总表
                if (CurNLoadData.Count>0)
                    _NLoadData.Add(lc.LCName, CurNLoadData);                
            }
        }

        /// <summary>
        /// 按比例系数修改节点荷载
        /// </summary>
        /// <param name="node">节点号</param>
        /// <param name="LC_Name">工况名</param>
        /// <param name="factor">比例系数</param>
        public void ModifyNodeLoad(int node, string LC_Name, double factor)
        {
            //如果没有此工况请返回
            if (_NLoadData.ContainsKey(LC_Name) == false)
                return;
            SortedList<int, BNLoad> NLoadData = _NLoadData[LC_Name] as SortedList<int, BNLoad>;
            if (NLoadData.ContainsKey(node) == false)
                return;
            else
            {
                NLoadData[node].Magnify(factor);
            }
        }
        #endregion
    }
    #endregion

    #region Geometry Model Class(几何模型类)
    /// <summary>
    /// 定义存储文件信息的节点类：Bnodes
    /// </summary>
    [Serializable]
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
        /// 节点坐标位置
        /// </summary>
        public Point3d Location
        {
            get
            {
                Point3d res = new Point3d(X, Y, Z);
                return res;
            }
        }
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

        #region 方法函数
        //重载ToString函数
        new public string ToString()
        {
            return ("(" + num.ToString() + "," + X.ToString() + "," + Y.ToString() + "," + Z.ToString() + ")");
        }

        /// <summary>
        /// 求两节点间的矩离
        /// </summary>
        /// <param name="nodeNext">下一个节点</param>
        /// <returns>返回距离值</returns>
        public double DistanceTo(Bnodes nodeNext)
        {
            double res = Math.Sqrt(Math.Pow((nodeNext.X - this.X), 2) +
                Math.Pow((nodeNext.Y - this.Y), 2) +
                Math.Pow((nodeNext.Z - this.Z), 2));
            return res;
        }

        /// <summary>
        /// 求到另一节点的方向向量
        /// </summary>
        /// <param name="nodeto">到的节点</param>
        /// <returns>单位方向向量</returns>
        public Vector3 VectorTo(Bnodes nodeto)
        {
            Vector3 v1 = new Vector3(this.X, this.Y, this.Z);
            Vector3 v2 = new Vector3(nodeto.X, nodeto.Y, nodeto.Z);
            Vector3 Res = v2 - v1;//矢量相减即得方向向量
            Res.Normalize();//归一化
            return Res;
        }
        #endregion
        
    }

    /// <summary>
    /// 单元基类
    /// </summary>
    [Serializable]
    public abstract class Element : Object
    {
        private int _iEL, _iMAT, _iPRO;
        private ElemType _TYPE;
        private CoordinateSystem _ECS;//单元坐标系
        /// <summary>
        /// 单元节点号数组
        /// </summary>
        public List<int> iNs;
        #region 属性
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
        public ElemType TYPE
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
        /// 单元局部坐标系
        /// </summary>
        public CoordinateSystem ECS
        {
            get { return _ECS; }
            set { _ECS = value; }
        }

        /// <summary>
        /// 节点数
        /// </summary>
        public int NodeCount
        {
            get { return iNs.Count; }
        }
        #endregion
        
        /// <summary>
        ///构造函数 
        /// </summary>
        public Element()
        {
            iEL = 0;
            TYPE = ElemType.NOTYPE;
            iMAT = 0;
            iPRO = 0;
            iNs = new List<int>();
            _ECS = new CoordinateSystem();
        }
        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="num">单元编号</param>
        /// <param name="type">单元类型</param>
        /// <param name="mat">单元材料号</param>
        /// <param name="pro">单元特性值号，或截面号</param>
        /// <param name="iNodes">节点号数组</param>
        public Element(int num, ElemType type, int mat, int pro, params int[] iNodes)
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
            _ECS = new CoordinateSystem();
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
    [Serializable]
    public class FrameElement : Element
    {
        private double Angle;
        private int _iSUB;
        private double _EXVAL;
        private DesignParameters _DPs;
        /// <summary>
        /// 其它参数3
        /// </summary>
        public int iOPT;

        #region 属性
        /// <summary>
        /// 钢结构梁单元的设计参数
        /// </summary>
        public DesignParameters DPs
        {
            get { return _DPs; }
            set { _DPs = value; }
        }

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
        /// 子类型（BEAM和TRUSS无关）
        /// </summary>
        public int iSUB
        {
            set { _iSUB = value; }
            get { return _iSUB; }
        }
        /// <summary>
        /// 单元另行输入的数据（BEAM和TRUSS无关）
        /// </summary>
        public double EXVAL
        {
            set { _EXVAL = value; }
            get { return _EXVAL; }
        }

        /// <summary>
        /// 单元节点号i
        /// </summary>
        public int I
        {
            get 
            {
                return iNs[0]; 
            }
        }

        /// <summary>
        /// 单元节点号j
        /// </summary>
        public int J
        {
            get
            {
                return iNs[1];
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 不带参数的构造函数,beta=0;type="BEAM"
        /// </summary>
        public FrameElement()
            : base()
        {
            this.TYPE = ElemType.BEAM;
            this.beta = 0;
            this._DPs = new DesignParameters();
        }
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="num">单元号</param>
        /// <param name="type">单元类型</param>
        /// <param name="mat">材料号</param>
        /// <param name="pro">截面特性号</param>
        /// <param name="iNodes">节点号数组</param>
        public FrameElement(int num, ElemType type, int mat, int pro, params int[] iNodes)
            : base(num, type, mat, pro, iNodes)
        {
            //调用基类的构造函数
            this._DPs = new DesignParameters();
        }
        #endregion

        #region 单元方法
        #endregion
    }

    /// <summary>
    /// 平面单元类
    /// </summary>
    [Serializable]
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
            this.TYPE = ElemType.PLATE;
        }

        /// <summary>
        /// 调用基类的构造函数
        /// </summary>
        /// <param name="num">单元号</param>
        /// <param name="type">单元类型</param>
        /// <param name="mat">材料号</param>
        /// <param name="pro">截面号，厚度号</param>
        /// <param name="iNodes">节点号数组</param>
        public PlanarElement(int num, ElemType type, int mat, int pro, params int[] iNodes)
            : base(num, type, mat, pro, iNodes)
        {
            //调用基类的对应构造函数
        }
    }

    /// <summary>
    /// 存储截面特性的基类
    /// </summary>
    [Serializable]
    public abstract class BSections
    {
        /// <summary>
        /// 截面号
        /// </summary>
        private int iSEC;
        /// <summary>
        /// 截面类型：枚举
        /// </summary>
       // public string TYPE;
        public SecType TYPE;
        /// <summary>
        /// 截面名称
        /// </summary>
        private string SNAME;
        /// <summary>
        /// 截面偏移数据
        /// </summary>
        public ArrayList OFFSET;
        /// <summary>
        /// 是否考虑剪切变形
        /// </summary>
        public bool bsd;
        /// <summary>
        /// 截面形状：B表示箱形
        /// </summary>
        public SecShape SSHAPE;
        /// <summary>
        /// 截面数据信息
        /// </summary>
        public ArrayList SEC_Data;


        #region 存储截面特性值
        protected double _Area;//面积
        protected double _ASy;//单元坐标系y轴方向的有效剪切面积
        protected double _ASz;//单元坐标系z轴方向的有效剪切面积
        protected double _Ixx;//截面扭转贯性矩
        /// <summary>
        /// 截面扭转贯性矩
        /// </summary>
        public double Ixx
        {
            get { return _Ixx; }
            set { _Ixx = value; }
        }
        protected double _Iyy;//单元绕y轴的截面贯性矩
        /// <summary>
        /// 单元绕y轴的截面贯性矩
        /// </summary>
        public  double Iyy
        {
            get { return _Iyy; }
            set { _Iyy = value; }
        }
        protected double _Izz;//单元绕z轴的截面贯性矩
        /// <summary>
        /// 单元绕z轴的截面贯性矩
        /// </summary>
        public double Izz
        {
            get { return _Izz; }
            set { _Izz = value; }
        }

        private double _Iw;
        /// <summary>
        /// 毛截面扇性惯性矩
        /// </summary>
        public double Iw
        {
            get { return _Iw; }
            set { _Iw = value; }
        }

        protected double _CyP;//自中和轴到单元坐标系(+)y方向最外端的距离
        /// <summary>
        /// 自中和轴到单元坐标系(+)y方向最外端的距离
        /// </summary>
        public double CyP
        {
            get { return _CyP; }
            set { _CyP = value; }
        }
        protected double _CyM;//自中和轴到单元坐标系(-)y方向最外端的距离
        /// <summary>
        /// 自中和轴到单元坐标系(-)y方向最外端的距离
        /// </summary>
        public double CyM
        {
            get { return _CyM; }
            set { _CyM = value; }
        }
        protected double _CzP;//自中和轴到单元坐标系(+)z方向最外端的距离
        /// <summary>
        /// 自中和轴到单元坐标系(+)z方向最外端的距离
        /// </summary>
        public double CzP
        {
            get { return _CzP; }
            set { _CzP = value; }
        }
        protected double _CzM;//自中和轴到单元坐标系(-)z方向最外端的距离
        /// <summary>
        /// 自中和轴到单元坐标系(-)z方向最外端的距离
        /// </summary>
        public double CzM
        {
            get { return _CzM; }
            set { _CzM = value; }
        }
        protected double _QyB;//作用于单元坐标系y轴方向的剪切系数
        protected double _QzB;//作用于单元坐标系z轴方向的剪切系数
        protected double _PERI_OUT;//截面外轮廓周长
        protected double _PERI_IN;//截面内轮廓周长
        private double _Cy;//截面形心y坐标
        /// <summary>
        /// 截面形心y坐标
        /// </summary>
        public double Cy
        {
            get { return _Cy; }
            set { _Cy = value; }
        }
        private double _Cz;//截面形心z坐标
        /// <summary>
        /// 截面形心z坐标
        /// </summary>
        public  double Cz
        {
            get { return _Cz; }
            set { _Cz = value; }
        }
        private double _Sy;//截面剪心y坐标
        /// <summary>
        /// 截面剪心y坐标
        /// </summary>
        public double Sy
        {
            get { return _Sy; }
            set { _Sy = value; }
        }
        private double _Sz;//截面剪心z坐标
        /// <summary>
        /// 截面剪心z坐标
        /// </summary>
        public double Sz
        {
            get { return _Sz; }
            set { _Sz = value; }
        }

        protected double _y1;//四个角点坐标
        //四个角点坐标
        public double Y1
        {
            get { return _y1; }
            set { _y1 = value; }
        }
        protected double _z1;//四个角点坐标
        //四个角点坐标
        public  double Z1
        {
            get { return _z1; }
            set { _z1 = value; }
        }
        protected double _y2;//四个角点坐标
        //四个角点坐标
        public  double Y2
        {
            get { return _y2; }
            set { _y2 = value; }
        }
        protected double _z2;//四个角点坐标
        //四个角点坐标
        public  double Z2
        {
            get { return _z2; }
            set { _z2 = value; }
        }
        protected double _y3;//四个角点坐标
        //四个角点坐标
        public  double Y3
        {
            get { return _y3; }
            set { _y3 = value; }
        }
        protected double _z3;//四个角点坐标
        //四个角点坐标
        public  double Z3
        {
            get { return _z3; }
            set { _z3 = value; }
        }
        protected double _y4;//四个角点坐标
        //四个角点坐标
        public  double Y4
        {
            get { return _y4; }
            set { _y4 = value; }
        }
        protected double _z4;//四个角点坐标
        //四个角点坐标
        public  double Z4
        {
            get { return _z4; }
            set { _z4 = value; }
        }
        #endregion
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
        /// <summary>
        /// 面积特性
        /// </summary>
        public double Area
        {
            get { return _Area; }
            set { _Area = value; }
        }

        /// <summary>
        /// 截面验算点集合（目前内有4个点集）
        /// </summary>
        public Point2dCollection CheckPointCollection
        {
            get
            {
                Point2dCollection ptc = new Point2dCollection();
                ptc.addPt(new Point2d(_y1, _z1));
                ptc.addPt(new Point2d(_y2, _z2));
                ptc.addPt(new Point2d(_y3, _z3));
                ptc.addPt(new Point2d(_y4, _z4));
                return ptc;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BSections()
        {
            iSEC = 1;
            TYPE = SecType.DBUSER;
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
            SSHAPE = SecShape.P;

            SEC_Data = new ArrayList();
            SEC_Data.Add(1);
            SEC_Data.Add("GB-YB");
            SEC_Data.Add("P 180x10");
        }
        /// <summary>
        /// 按一定格式输出截面数据信息
        /// </summary>
        /// <returns>ansys命令流字符串</returns>
        public abstract string WriteData();

        /// <summary>
        /// 重新按数据计算截面特性：面积、贯性矩等
        /// </summary>
        public abstract void  CalculateSecProp();

        /// <summary>
        /// 设置截面常用特性值1
        /// </summary>
        /// <param name="area">面积</param>
        /// <param name="asy">y向有效剪切面积</param>
        /// <param name="asz">z向有效剪切面积</param>
        /// <param name="ixx">扭转贯性矩</param>
        /// <param name="iyy">绕y轴的贯性矩</param>
        /// <param name="izz">绕z轴的贯性矩</param>
        public void setSecProp1(double area, double asy, double asz, double ixx, double iyy, double izz)
        {
            _Area = area; _ASy = asy; _ASz = asz;
            _Ixx = ixx; _Iyy = iyy; _Izz = izz;
        }

        /// <summary>
        /// 设置截面常用特性值2
        /// </summary>
        /// <param name="cyp">自中和轴到单元坐标系(+)y方向最外端的距离</param>
        /// <param name="cym">自中和轴到单元坐标系(-)y方向最外端的距离</param>
        /// <param name="czp">自中和轴到单元坐标系(+)z方向最外端的距离</param>
        /// <param name="czm">自中和轴到单元坐标系(-)z方向最外端的距离</param>
        /// <param name="qyb">作用于单元坐标系y轴方向的剪切系数</param>
        /// <param name="qzb">作用于单元坐标系z轴方向的剪切系数</param>
        /// <param name="p_out">截面外轮廓周长</param>
        /// <param name="p_in">截面内轮廓周长</param>
        /// <param name="cy">截面形心y坐标</param>
        /// <param name="cz">截面形心y坐标</param>
        public void setSecProp2(double cyp, double cym, double czp, double czm, double qyb, double qzb,
            double p_out, double p_in, double cy, double cz)
        {
            _CyP = cyp; _CyM = cym; _CzP = czp; _CzM = czm; _QyB = qyb; _QzB = qzb;
            _PERI_OUT = p_out; _PERI_IN = p_in;
        }

        /// <summary>
        /// 设置截面常用特性值3
        /// </summary>
        /// <param name="y1">左上点y坐标</param>
        /// <param name="z1">左上点z坐标</param>
        /// <param name="y2">右上点y坐标</param>
        /// <param name="z2">右上点z坐标</param>
        /// <param name="y3">左下点y坐标</param>
        /// <param name="z3">左下点z坐标</param>
        /// <param name="y4">右下点y坐标</param>
        /// <param name="z4">右下点z坐标</param>
        public void setSecProp3(double y1, double z1, double y2, double z2, double y3, double z3, double y4, double z4)
        {
            _y1 = y1; _z1 = z1; _y2 = y2; _z2 = z2;
            _y3 = y3; _z3 = z3; _y4 = y4; _z4 = z4;
        }
        /// <summary>
        /// 取得截面的指定验算点
        /// </summary>
        /// <param name="iPt">验算点号，目前只有1~4，共4个点数据</param>
        /// <param name="Y">验算点Y坐标</param>
        /// <param name="Z">验算点Z坐标</param>
        public void getCheckPoint(int iPt, out double Y, out double Z)
        {
            switch (iPt)
            {
                case 1: Y = _y1; Z = _z1; break;
                case 2: Y = _y2; Z = _z2; break;
                case 3: Y = _y3; Z = _z3; break;
                case 4: Y = _y4; Z = _z4; break;
                default: Y = _y1; Z = _z1; break;
            }
        }
    }

    /// <summary>
    /// 常用截面信息类
    /// </summary>
    [Serializable]
    public class SectionDBuser : BSections
    {
        public SectionDBuser():base()
        {
            this.TYPE = SecType.DBUSER;
            //更新截面特性
            CalculateSecProp();
        }

        /// <summary>
        /// 重载输出ansys 命令流函数
        /// </summary>
        /// <returns>ansys命令流</returns>
        public override string WriteData()
        {
            //throw new NotImplementedException();
            string res = null;
            if (this.SSHAPE == SecShape.B && (int)this.SEC_Data[0] == 2)//箱形截面
            {
                res += "sectype," + this.Num.ToString() + ",beam,hrec," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString() + "," + SEC_Data[3].ToString() + "," + SEC_Data[3].ToString()
                + "," + SEC_Data[6].ToString() + "," + SEC_Data[4].ToString();
            }
            else if (this.SSHAPE== SecShape.H && (int)this.SEC_Data[0] == 2)//H型截面
            {
                res += "sectype," + this.Num.ToString() + ",beam,i," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString() + "," + SEC_Data[4].ToString()
                    + "," + SEC_Data[4].ToString() + "," + SEC_Data[3].ToString();
            }
            else if (this.SSHAPE == SecShape.P && (int)this.SEC_Data[0] == 2)//圆管截面
            {
                double ri = (double)SEC_Data[1] / 2 - (double)SEC_Data[2];
                double ro = (double)SEC_Data[1] / 2;
                res += "sectype," + this.Num.ToString() + ",beam,ctube," + this.Name;
                res += "\nsecdata," + ri.ToString() + "," + ro.ToString();
            }
            else if (this.SSHAPE==SecShape .SB&& (int)this.SEC_Data[0] == 2)//矩形截面
            {
                res += "sectype," + this.Num.ToString() + ",beam,rect," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString();
            }
            else if (this.SSHAPE== SecShape .T&& (int)this.SEC_Data[0] == 2)//T型截面
            {
                res += "sectype," + this.Num.ToString() + ",beam,t," + this.Name;
                res += "\nsecdata," + SEC_Data[1].ToString() + "," + SEC_Data[2].ToString() + "," + SEC_Data[3].ToString() + "," +
                    SEC_Data[4].ToString();
            }
            else if (this.SSHAPE==SecShape.SR && (int)this.SEC_Data[0] == 2)//圆形截面
            {
                res += "sectype," + this.Num.ToString() + ",beam,csolid," + this.Name;
                res += "\nsecdata," + ((double)SEC_Data[1] / 2).ToString();
            }
            else if (this.SSHAPE==SecShape.C&& (int)this.SEC_Data[0]==2)//槽钢截面
            {
                res += "sectype," + this.Num.ToString() + ",beam,chan," + this.Name;
                res += "\nsecdata," + SEC_Data[5].ToString() + "," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString() + "," +
                    SEC_Data[6].ToString() + "," + SEC_Data[4].ToString() + "," + SEC_Data[3].ToString();
            }
            else
            {
                res += "!此截面形状信息未处理：" + this.Num.ToString();
            }
            return res;
        }

        /// <summary>
        /// 重载计算截面特性
        /// </summary>
        public override void CalculateSecProp()
        {
            //throw new NotImplementedException();
            if (this.SSHAPE == SecShape.B && (int)this.SEC_Data[0] == 2)//箱形截面
            {
                double D_h = Convert.ToDouble(SEC_Data[1]);
                double D_b = Convert.ToDouble(SEC_Data[2]);
                double D_tw = Convert.ToDouble(SEC_Data[3]);
                double D_tf = Convert.ToDouble(SEC_Data[4]);
                this._Area = D_h*D_b-(D_h-2*D_tf)*(D_b-2*D_tw);//面积
                this._ASy = 2 * D_b * D_tf;//有效剪切面积
                this._ASz = 2 * D_h * D_tw;//有效剪切面积
                this._Ixx = 2 * Math.Pow((D_b-D_tw) * (D_h-D_tf), 2) / ((D_b-D_tw) / D_tf + (D_h-D_tf) / D_tw);//抗扭刚度
                this._Iyy = D_b * Math.Pow(D_h, 3) / 12 - (D_b - 2 * D_tw) * Math.Pow(D_h - 2 * D_tf, 3) / 12;//抗弯刚度
                this._Izz = D_h * Math.Pow(D_b, 3) / 12 - (D_h - 2 * D_tf) * Math.Pow(D_b - 2 * D_tw, 3) / 12;//抗弯刚度
                this.Cy = D_b / 2;
                this.Cz = D_h / 2;
                this._y1 = -D_b / 2;
                this._z1 = D_h / 2;
                this._y2 = D_b / 2;
                this._z2 = D_h / 2;
                this._y3 = D_b / 2;
                this._z3 = -D_h / 2;
                this._y4 = -D_b / 2;
                this._z4 = -D_h / 2;
                //todo:剪切系数和内外表面积未计算
            }
            else if (this.SSHAPE == SecShape.H && (int)this.SEC_Data[0] == 2)//H型截面
            {
                double h = Convert.ToDouble(SEC_Data[1]);
                double b = Convert.ToDouble(SEC_Data[2]);
                double tw = Convert.ToDouble(SEC_Data[3]);
                double tf = Convert.ToDouble(SEC_Data[4]);
                this._Area = h*b-(h-2*tf)*(b-tw);//面积
                this._ASy = 5*(2*b*tf)/6;//有效剪切面积
                this._ASz = h*tw;//有效剪切面积
                this._Ixx = (h*Math.Pow(tw,3)+2*b*Math.Pow(tf,3))/3;//抗扭刚度
                this._Iyy = b * Math.Pow(h, 3) / 12 - (b - tw) * Math.Pow(h - 2 * tf, 3) / 12;//抗弯刚度
                this._Izz = 2*tf * Math.Pow(b, 3) / 12 +(h-2*tf)*Math.Pow(tw,3)/12;//抗弯刚度
                this.Cy = b / 2;
                this.Cz = h / 2;
                this._y1 = -b / 2;
                this._z1 = h / 2;
                this._y2 = b / 2;
                this._z2 = h / 2;
                this._y3 = b / 2;
                this._z3 = -h / 2;
                this._y4 = -b / 2;
                this._z4 = -h / 2;
                //todo:剪切系数和内外表面积未计算
            }
            else if (this.SSHAPE == SecShape.P && (int)this.SEC_Data[0] == 2)//圆管截面
            {
                double tw=Convert.ToDouble(SEC_Data[2]);//壁厚
                double ri = (double)SEC_Data[1] / 2 - tw;
                double ro = (double)SEC_Data[1] / 2;
                this._Area = Math.PI * Math.Pow(ro, 2) - Math.PI * Math.Pow(ri, 2);
                this._ASy = Math.PI * (ri + tw / 2) * tw;//有效剪切面积
                this._ASz = Math.PI * (ri + tw / 2) * tw; ;//有效剪切面积
                this._Ixx = Math.PI*(Math.Pow(ro,4)-Math.Pow(ri,4))/2;//抗扭刚度
                this._Iyy = Math.PI * Math.Pow(2 * ro, 4) / 64 - Math.PI * Math.Pow(2 * ri, 4) / 64;//抗弯刚度
                this._Izz = Math.PI * Math.Pow(2 * ro, 4) / 64 - Math.PI * Math.Pow(2 * ri, 4) / 64;//抗弯刚度
                this.Cy = ro;
                this.Cz = ro;
                this._y1 = 0;
                this._z1 =ro;
                this._y2 = ro;
                this._z2 = 0;
                this._y3 =0;
                this._z3 = -ro;
                this._y4 = -ro;
                this._z4 = 0;
            }
            else if (this.SSHAPE == SecShape.SB && (int)this.SEC_Data[0] == 2)//矩形截面
            {
                double D_h = Convert.ToDouble(SEC_Data[1]);
                double D_b = Convert.ToDouble(SEC_Data[2]);
                this._Area = D_h * D_b;//面积
                this._ASy = 5 * D_b * D_h / 6;//有效剪切面积
                this._ASz = 5 * D_b * D_h / 6;//有效剪切面积
                this._Ixx =0;//抗扭刚度
                this._Iyy = D_b * Math.Pow(D_h, 3) / 12 ;//抗弯刚度
                this._Izz = D_h * Math.Pow(D_b, 3) / 12 ;//抗弯刚度
                this.Cy = D_b / 2;
                this.Cz = D_h / 2;
                this._y1 = -D_b / 2;
                this._z1 = D_h / 2;
                this._y2 = D_b / 2;
                this._z2 = D_h / 2;
                this._y3 = D_b / 2;
                this._z3 = -D_h / 2;
                this._y4 = -D_b / 2;
                this._z4 = -D_h / 2;
                //todo:面积计算实现
            }
            else if (this.SSHAPE == SecShape.T && (int)this.SEC_Data[0] == 2)//T型截面
            {
                this._Area = 0;
                //todo:面积计算实现
            }
            else if (this.SSHAPE == SecShape.SR && (int)this.SEC_Data[0] == 2)//圆形截面
            {
                double rr = (double)SEC_Data[1] / 2;
                this._Area = Math.PI * Math.Pow(rr, 2);
            }
            else if (this.SSHAPE == SecShape.C && (int)this.SEC_Data[0] == 2)//槽钢截面
            {
                this._Area = 0;
                //todo:面积计算实现
            }
            else
            {
                this._Area=0;
            }
        }
    }

    /// <summary>
    /// 渐变截面信息类
    /// </summary>
    public class SectionTapered : BSections
    {
        /// <summary>
        /// 考虑单元坐标系y轴截面弯矩的方法
        /// </summary>
        public iVAR iyVAR;
        /// <summary>
        /// 考虑单元坐标系z轴截面弯矩的方法
        /// </summary>
        public iVAR izVAR;

        /// <summary>
        /// 子截面输入类型
        /// </summary>
        public STYPE SubTYPE;
        
        /// <summary>
        /// 不带参数的构造函数
        /// </summary>
        public SectionTapered()
            : base()
        {
            this.TYPE = SecType.TAPERED;
            iyVAR = iVAR.Linear;
            izVAR = iVAR.Linear;
            SubTYPE = STYPE.USER;

            //更新截面特性
            CalculateSecProp();
        }

        /// <summary>
        /// 重载输出函数
        /// </summary>
        /// <returns>ansys截面定义命令</returns>
        public override string WriteData()
        {
            //throw new NotImplementedException();
            string res = "!此截面为TAPERED截面输入，信息未处理:" + this.Num.ToString();
            return res;
        }
        
        /// <summary>
        /// 重载计算截面特性
        /// </summary>
        public override void CalculateSecProp()
        {
            //throw new NotImplementedException();
            this._Area = 0;
            //todo:面积计算实现
        }
    }

    /// <summary>
    /// 自定义SPC截面信息类
    /// </summary>
    [Serializable]
    public class SectionGeneral : BSections
    {
        private Point2dCollection _OPOLY;
        /// <summary>
        /// 外轮廓点集
        /// </summary>
        public Point2dCollection OPOLY
        {
            get { return _OPOLY; }
        }
        private List<Point2dCollection> _IPOLYs;
        /// <summary>
        /// 内轮廓点集
        /// </summary>
        public List<Point2dCollection> IPOLYs
        {
            get { return _IPOLYs; }
        }
        private bool _bBU;
        private bool _bEQ;


        /// <summary>
        /// 未知参数
        /// </summary>
        public bool bBU
        {
            set { _bBU = value; }
            get { return _bBU; }
        }

        /// <summary>
        /// 未知参数
        /// </summary>
        public bool bEQ
        {
            set { _bEQ = value; }
            get { return _bEQ; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public SectionGeneral()
            : base()
        {
            _OPOLY = new Point2dCollection();
            _IPOLYs = new List<Point2dCollection>();
            _bBU = true;
            _bEQ = true;
        }

        /// <summary>
        /// 输出Ansys截面信息
        /// </summary>
        /// <returns>APDL命令：截面类型 SectionGeneral</returns>
        public override string WriteData()
        {
            string res = null;
            res = "!此截面为SPC自定义截面";
            res += "\nsectype," + this.Num.ToString() + ",beam,mesh," + this.Name;
            res += "\nsecread,"+this.Name+",sect,,mesh";
            return res;
        }
        /// <summary>
        /// 生成自定义SPC截面的ansys宏文件
        /// </summary>
        /// <returns>宏命令流</returns>
        public string GetSectMac()
        {
            string res = null;
            res = "!"+this.Name+"截面为SPC自定义截面，用宏文件进行生成";
            res += "\n*create," + this.Name + ",sec";
            res += "\nfinish\n/clear\n/prep7";//清理模型
            res += "\n*get,kpmax,kp,0,num,maxd";//最大关键点号
            int i = 0;//编号
            //创建平面点
            foreach (Point2d pt in _OPOLY)
            {
                i++;
                res += "\nk," + "kpmax+" + i.ToString() + "," + pt.X.ToString() + "," + pt.Y.ToString();
            }
            //连接平面点为线
            for (int j = 0; j < _OPOLY.Length - 1; j++)
            {
                res += "\nl," + "kpmax+" + (j + 1).ToString() + "," + "kpmax+" + (j + 2).ToString();
            }
            res += "\nl," + "kpmax+" + i.ToString() + ",kpmax+1";//封闭曲线

            if (_IPOLYs.Count > 0)
            {
                foreach (Point2dCollection ptc in _IPOLYs)
                {
                    res += "\n!内轮廓";
                    res += "\n*get,kpmax,kp,0,num,maxd";//最大关键点号
                    i = 0;//归零
                    //创建平面点
                    foreach (Point2d pt in ptc)
                    {
                        i++;
                        res += "\nk," + "kpmax+" + i.ToString() + "," + pt.X.ToString() + "," + pt.Y.ToString();
                    }
                    //连接平面点为线
                    for (int j = 0; j < ptc.Length - 1; j++)
                    {
                        res += "\nl," + "kpmax+" + (j + 1).ToString() + "," + "kpmax+" + (j + 2).ToString();
                    }
                    res += "\nl," + "kpmax+" + i.ToString() + ",kpmax+1";//封闭曲线
                }
            }
            res += "\nlsel,all";
            res += "\nal,all\t!形成面";
            res += "\net,100,82";
            res += "\naatt,,,100";
            //设置网格划分尺寸
            //double ele = _OPOLY[0].DistantTo(_OPOLY[1])/2;//前两点距离的一半
            double ele = _CyM / 5;
            res += "\naesize,all,"+ele.ToString();
            res += "\namesh,all";
            res += "\nsecwrite," + this.Name + ",sect,,100" + "\t!输出截面文件";
            res += "\nkpmax=";
            res += "\n*end";
            return res;
        }
        /// <summary>
        /// 计算截面特性： SectionGeneral类截面什么也不做
        /// </summary>
        public override void CalculateSecProp()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 给外轮廓线增加控制点
        /// </summary>
        /// <param name="pt">点对</param>
        public void addtoOPOLY(Point2d pt)
        {
            _OPOLY.addPt(pt);
        }
        /// <summary>
        /// 给内轮廓线增加控制点
        /// </summary>
        /// <param name="index">内轮廓线索引号</param>
        /// <param name="pt">要增加的点对</param>
        public void addtoIPOLY(int index, Point2d pt)
        {
            if (_IPOLYs.Count > index)
            {
                _IPOLYs[index].addPt(pt);
            }
            else
            {
                Point2dCollection ptnew=new Point2dCollection ();
                ptnew.addPt(pt);
                _IPOLYs.Add(ptnew);
            }
        }
    }

    /// <summary>
    /// 存储板单元厚度信息的类
    /// </summary>
    [Serializable]
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

    /// <summary>
    /// 截面特性种类，枚举
    /// </summary>
    public enum SecType
    {
        /// <summary>
        /// 在DB中输入的，或者其它定型的截面
        /// </summary>
        DBUSER,
        /// <summary>
        /// 直接输入截面特性数据
        /// </summary>
        VALUE,
        /// <summary>
        /// SRC构件截面
        /// </summary>
        SRC,
        /// <summary>
        /// 组合截面
        /// </summary>
        COMBINED,
        /// <summary>
        /// 渐变截面
        /// </summary>
        TAPERED
    }

    /// <summary>
    /// 截面形状，枚举
    /// </summary>
    public enum SecShape
    {
        /// <summary>
        /// Angle 角钢
        /// </summary>
        L,
        /// <summary>
        /// Channel 槽钢 
        /// </summary>
        C,
        /// <summary>
        /// H型钢
        /// </summary>
        H,
        /// <summary>
        /// T型钢
        /// </summary>
        T,
        /// <summary>
        /// Box 箱形
        /// </summary>
        B,
        /// <summary>
        /// Pipe 钢管
        /// </summary>
        P,
        /// <summary>
        /// Solid Rectangle 实矩形
        /// </summary>
        SB,
        /// <summary>
        /// Solid Round 实圆形
        /// </summary>
        SR,
        /// <summary>
        /// Cold Formed Channel 冷弯槽钢
        /// </summary>
        CC,
        /// <summary>
        /// 自定义截面
        /// </summary>
        GEN
    }

    /// <summary>
    /// 考虑渐变截面贯性矩的方法（适用于TAPERED截面类型）
    /// </summary>
    public enum iVAR
    {
        /// <summary>
        /// 直线形
        /// </summary>
        Linear=1,
        /// <summary>
        /// 抛物线形
        /// </summary>
        Parabolic=2,
        /// <summary>
        /// 三次曲线形
        /// </summary>
        Cubic=3
    }

    /// <summary>
    /// 子截面形状数据输入类型（适用于TAPERED截面类型）
    /// </summary>
    public enum STYPE
    {
        /// <summary>
        /// 各国标准截面
        /// </summary>
        DB,
        /// <summary>
        /// 用户输入定型截面尺寸
        /// </summary>
        USER,
        /// <summary>
        /// 使用VALUE输入截面
        /// </summary>
        VALUE
    }

    /// <summary>
    /// 单元类型，枚举
    /// </summary>
    public enum ElemType
    {
        /// <summary>
        /// 桁架单元
        /// </summary>
        TRUSS,
        /// <summary>
        /// 梁单元
        /// </summary>
        BEAM,
        /// <summary>
        /// 只受拉单元
        /// </summary>
        TENSTR,
        /// <summary>
        /// 只受压单元
        /// </summary>
        COMPTR,
        /// <summary>
        /// 平面板单元
        /// </summary>
        PLATE,
        /// <summary>
        /// 平面应力单元
        /// </summary>
        PLSTRS,
        /// <summary>
        /// 平面应变单元
        /// </summary>
        PLSTRN,
        /// <summary>
        /// 轴对称单元
        /// </summary>
        AXISYM,
        /// <summary>
        /// 实体单元
        /// </summary>
        SOLID,
        /// <summary>
        /// 未知单元
        /// </summary>
        NOTYPE
    }
    #endregion

    #region Constraint (边界约束类)
    /// <summary>
    /// 边界条件类
    /// </summary>
    [Serializable]
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

    /// <summary>
    /// 材料特性值类
    /// </summary>
    [Serializable]
    public class BMaterial:Object 
    {
        private int _iMAT;//材料编号
        private MatType _TYPE;//材料类型
        private string _MNAME;//材料名称
        private double _Elast;//弹性模量
        private double _Poisn;//泊松比
        private double _Thermal;//线膨胀系数
        private double _Den;//单位体积重量
        private double _Fy;//材料屈服强度

        /// <summary>
        /// 原始mgt数据信息
        /// </summary>
        public ArrayList MGT_Data;

        /// <summary>
        /// 材料号
        /// </summary>
        public int iMAT
        {
            get { return _iMAT; }
        }
        /// <summary>
        /// 材料类型
        /// </summary>
        public MatType TYPE
        {
            get { return _TYPE; }
        }
        /// <summary>
        /// 材料名称
        /// </summary>
        public string MNAME
        {
            get { return _MNAME; }
        }

        /// <summary>
        /// 弹性模量
        /// </summary>
        public double Elast
        {
            get { return _Elast; }
        }
        /// <summary>
        /// 泊松比
        /// </summary>
        public double Poisn
        {
            get { return _Poisn; }
        }
        /// <summary>
        /// 线膨胀系数
        /// </summary>
        public double Thermal
        {
            get { return _Thermal; }
        }
        /// <summary>
        /// 单位体积重量
        /// </summary>
        public double Den
        {
            get { return _Den; }
        }

        /// <summary>
        /// 材料屈服强度
        /// </summary>
        public double Fy
        {
            get { return _Fy; }
        }
        /// <summary>
        /// 构造函数:默认是钢的数据
        /// </summary>
        public BMaterial()
        {
            _iMAT = 1;
            _TYPE = MatType.USER;
            _MNAME = "dafault";
            _Elast = 2.06e11;
            _Poisn = 0.3;
            _Thermal = 1.2e-5;
            _Den = 7850;
            _Fy = 235e6;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="num">材料号</param>
        /// <param name="type">材料类型</param>
        /// <param name="name">材料名称</param>
        public BMaterial(int num, MatType type, string name)
        {
            _iMAT = num;
            _TYPE = type;
            _MNAME = name;
            _Elast = 0;
            _Poisn = 0;
            _Thermal = 0;
            _Den = 0;
            _Fy = 0;
            MGT_Data = new ArrayList();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="num">材料号</param>
        /// <param name="type">材料类型</param>
        /// <param name="name">材料名称</param>
        /// <param name="E">弹模（Pa）</param>
        /// <param name="Poi">泊松比</param>
        /// <param name="Ther">线膨胀系数</param>
        /// <param name="den">质量密度（kg/m3）</param>
        public  BMaterial(int num,MatType type,string name,double E,double Poi,double Ther,double den)
        {

            _iMAT = num;
            _TYPE = type;
            _MNAME = name;
            _Elast = E;
            _Poisn = Poi;
            _Thermal = Ther;
            _Den = den;
            MGT_Data = new ArrayList();
        }

        //方法
        /// <summary>
        /// 设置弹模等基本数据
        /// </summary>
        /// <param name="E">弹模（Pa）</param>
        /// <param name="Poi">泊松比</param>
        /// <param name="Ther">线膨胀系数</param>
        /// <param name="den">质量密度（kg/m3）</param>
        public void setProp(double E, double Poi, double Ther, double den)
        {
            _Elast = E;
            _Poisn = Poi;
            _Thermal = Ther;
            _Den = den;
        }

        /// <summary>
        /// 存储MGT文件原始记录信息
        /// </summary>
        /// <param name="data">mgt文件数据行</param>
        public void addMGTdata(string data)
        {
            string[] temp = data.Split(',');

            foreach (string dt in temp)
            {
                MGT_Data.Add(dt.Trim());
            }
        }
        /// <summary>
        /// 标准化材料特性值
        /// </summary>
        public void NormalizeProp()
        {
            string s1=this.MGT_Data[MGT_Data.Count-2] as string;
            string s2=this.MGT_Data[MGT_Data.Count-1] as string;
            if (this._TYPE == MatType.STEEL &&( s1 == "GB03(S)"))
            {
                if (s2 == "Q345")
                {
                    _Fy = 345e6;
                }
                else if (s2 == "Q235")
                {
                    _Fy = 235e6;
                }
                else if (s2 == "Q390")
                {
                    _Fy = 390e6;
                }
                else if (s2 == "Q420")
                {
                    _Fy = 420e6;
                }
            }
        }
    }

    /// <summary>
    /// 材料类型，枚举
    /// </summary>
    public enum MatType
    {
        /// <summary>
        /// 钢
        /// </summary>
        STEEL,
        /// <summary>
        /// 混凝土
        /// </summary>
        CONC,
        /// <summary>
        /// 钢与混凝土
        /// </summary>
        SRC,
        /// <summary>
        /// 用户自定义
        /// </summary>
        USER
    }
    #endregion

    #region 单元内力类
    /// <summary>
    /// 单元截面内力类
    /// </summary>
    [Serializable]
    public class SecForce
    {
        private double _N, _T, _Vy, _Vz, _My, _Mz;
        /// <summary>
        /// 轴向力(拉为正，压为负)
        /// </summary>
        public double N
        {
            get { return _N; }
        }
        /// <summary>
        /// 扭矩
        /// </summary>
        public double T
        {
            get { return _T; }
        }
        /// <summary>
        /// 沿单元y轴的剪力
        /// </summary>
        public double Vy
        {
            get { return _Vy; }
        }
        /// <summary>
        /// 沿单元z轴的剪力
        /// </summary>
        public double Vz
        {
            get { return _Vz; }
        }
        /// <summary>
        /// 绕单元y轴的弯矩
        /// </summary>
        public double My
        {
            get { return _My; }
        }
        /// <summary>
        /// 绕单元z轴的弯矩
        /// </summary>
        public double Mz
        {
            get { return _Mz; }
        }

        /// <summary>
        /// 构造函数1
        /// </summary>
        public SecForce()
        {
            this.SetAllForces(0, 0, 0, 0, 0, 0);
        }
        /// <summary>
        /// 构造函数2
        /// </summary>
        /// <param name="N">轴力/kN/m</param>
        /// <param name="T">扭矩/kN/m</param>
        /// <param name="Vy">剪力/kN/m</param>
        /// <param name="Vz">剪力/kN/m</param>
        /// <param name="My">弯矩/kN/m</param>
        /// <param name="Mz">弯矩/kN/m</param>
        public SecForce(double N, double T, double Vy, double Vz, double My, double Mz)
        {
            this.SetAllForces(N, T, Vy, Vz, My, Mz);
        }
        /// <summary>
        /// 指定截面内力
        /// </summary>
        /// <param name="N">轴力/kN/m</param>
        /// <param name="T">扭矩/kN/m</param>
        /// <param name="Vy">剪力/kN/m</param>
        /// <param name="Vz">剪力/kN/m</param>
        /// <param name="My">弯矩/kN/m</param>
        /// <param name="Mz">弯矩/kN/m</param>
        public void SetAllForces(double N,double T,double Vy,double Vz,double My,double Mz)
        {
            _N = N; _T = T;
            _Vy = Vy; _Vz = Vz;
            _My = My; _Mz = Mz;
        }

        /// <summary>
        /// 截面内力相加重载方法
        /// </summary>
        /// <param name="sf1">截面内力1</param>
        /// <param name="sf2">截面内力2</param>
        /// <returns>相加后的截面内力</returns>
        public static SecForce operator +(SecForce sf1, SecForce sf2)
        {
            SecForce res = new SecForce();
            res.SetAllForces(sf1.N + sf2.N, sf1.T + sf2.T, sf1.Vy + sf2.Vy,
                sf1.Vz + sf2.Vz, sf1.My + sf2.My, sf1.Mz + sf2.Mz);
            return res;
        }

        /// <summary>
        /// 截面内力自乘系数
        /// </summary>
        /// <param name="fact">因子</param>
        /// <returns>截面内力</returns>
        public  SecForce Mutiplyby(double fact)
        {
            SecForce res = new SecForce(N * fact, T * fact, Vy * fact,
                Vz * fact, My * fact, Mz * fact);
            return res;
        }

        /// <summary>
        /// 截面内力进行指数运算
        /// </summary>
        /// <param name="mi">幂指数</param>
        /// <returns>新的截面内力</returns>
        public SecForce POW(double mi)
        {
            SecForce Res = new SecForce(Math.Pow(_N, mi), Math.Pow(_T, mi),
                Math.Pow(_Vy, mi), Math.Pow(_Vz, mi), Math.Pow(_My, mi),
                Math.Pow(_Mz, mi));
            return Res;
        }
    }
    /// <summary>
    /// 存储单元内力的类
    /// </summary>
    [Serializable]
    public class ElemForce
    {
        private SecForce _Force_i;
        private SecForce _Force_18;
        private SecForce _Force_28;
        private SecForce _Force_38;
        private SecForce _Force_48;
        private SecForce _Force_58;
        private SecForce _Force_68;
        private SecForce _Force_78;
        private SecForce _Force_j;
        #region 类属性
        /// <summary>
        /// 单元i处截面内力
        /// </summary>
        public SecForce Force_i
        {
            get { return _Force_i; }
        }
        /// <summary>
        /// 单元1/8处截面内力
        /// </summary>
        public SecForce Forcce_18
        {
            get { return _Force_18; }
        }
        /// <summary>
        /// 单元2/8处截面内力
        /// </summary>
        public SecForce Force_28
        {
            get { return _Force_28; }
        }
        /// <summary>
        /// 单元3/8处截面内力
        /// </summary>
        public SecForce Force_38
        {
            get { return _Force_38; }
        }
        /// <summary>
        /// 单元中点截面处的内力
        /// </summary>
        public SecForce Force_48
        {
            get { return _Force_48; }
        }
        /// <summary>
        /// 单元5/8处截面内力
        /// </summary>
        public SecForce Force_58
        {
            get { return _Force_58; }
        }
        /// <summary>
        /// 单元6/8处截面内力
        /// </summary>
        public SecForce Force_68
        {
            get { return _Force_68; }
        }
        /// <summary>
        /// 单元7/8处的截面内力
        /// </summary>
        public SecForce Force_78
        {
            get { return _Force_78; }
        }
        /// <summary>
        /// 单元j端截面内力
        /// </summary>
        public SecForce Force_j
        {
            get { return _Force_j; }
        }
        #endregion
        #region 类方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public ElemForce()
        {
            SecForce sf = new SecForce();
            for (int i = 0; i < 9; i++)
            {
                this[i] = sf;
            }
        }
        /// <summary>
        /// 输入单元内力
        /// </summary>
        /// <param name="Fi">单元i端截面内力</param>
        /// <param name="Fj">单元j端截面内力</param>
        public void SetElemForce(SecForce Fi, SecForce Fj)
        {
            _Force_i = Fi;
            _Force_j = Fj;
        }
        /// <summary>
        /// 输入单元内力（三个截面）
        /// </summary>
        /// <param name="Fi">单元i端截面内力</param>
        /// <param name="F48">单元中截面内力</param>
        /// <param name="Fj">单元j端截面内力</param>
        public void SetElemForce(SecForce Fi, SecForce F48, SecForce Fj)
        {
            _Force_i = Fi; _Force_j = Fj;
            _Force_48 = F48;
        }
        /// <summary>
        /// 输入单元内力，每次一个截面
        /// </summary>
        /// <param name="F">要输入的截面内力</param>
        /// <param name="num">截面号：0代表i端截面，8代表j端截面</param>
        public void SetElemForce(SecForce F, int num)
        {
            switch (num)
            {
                case 0: _Force_i = F; break;
                case 1: _Force_18 = F; break;
                case 2: _Force_28 = F; break;
                case 3: _Force_38 = F; break;
                case 4: _Force_48 = F; break;
                case 5: _Force_58 = F; break;
                case 6: _Force_68 = F; break;
                case 7: _Force_78 = F; break;
                case 8: _Force_j = F; break;
                default: break;
            }
        }


        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">索引号</param>
        /// <returns>返回截面内力</returns>
        public SecForce this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:return _Force_i;
                    case 1:return _Force_18;
                    case 2:return _Force_28; 
                    case 3:return _Force_38;
                    case 4:return _Force_48; 
                    case 5:return _Force_58; 
                    case 6:return _Force_68; 
                    case 7:return _Force_78; 
                    case 8:return _Force_j;
                    default: return new SecForce();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: _Force_i = value; break;
                    case 1: _Force_18 = value; break;
                    case 2: _Force_28 = value; break;
                    case 3: _Force_38 = value; break;
                    case 4: _Force_48 = value; break;
                    case 5: _Force_58 = value; break;
                    case 6: _Force_68 = value; break;
                    case 7: _Force_78 = value; break;
                    case 8: _Force_j =value; break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 重载单元内力相加运算符
        /// </summary>
        /// <param name="ef1">单元内力1</param>
        /// <param name="ef2">单元内力2</param>
        /// <returns>相加后的单元内力</returns>
        public static ElemForce operator +(ElemForce ef1, ElemForce ef2)
        {
            ElemForce res = new ElemForce();
            for (int i = 0; i < 9; i++)
            {
                res[i] = ef1[i] + ef2[i];
            }
            return res;
        }

        /// <summary>
        /// 单元内力自乘系数
        /// </summary>
        /// <param name="fact">系数</param>
        /// <returns>单元内力</returns>
        public ElemForce Mutiplyby(double fact)
        {
            ElemForce res = new ElemForce();
            for (int i = 0; i < 9; i++)
            {
                res[i]=this[i].Mutiplyby(fact);
            }
            return res;
        }
        /// <summary>
        /// 单元内力进行指数运算
        /// </summary>
        /// <param name="mi">幂指数</param>
        /// <returns>新的单元内力</returns>
        public ElemForce Pow(double mi)
        {
            ElemForce res = new ElemForce();
            for (int i = 0; i < 9; i++)
            {
                res[i] = this[i].POW(mi);
            }
            return res;
        }
        #endregion
    }

    /// <summary>
    /// 单元内力表
    /// </summary>
    [Serializable]
    public class BElemForceTable:Object
    {
        private int _elem;
        private SortedList<string,ElemForce> _LCForces;

        /// <summary>
        /// 单元号
        /// </summary>
        public int elem
        {
            get { return _elem; }
            set { _elem = value; }
        }

        /// <summary>
        /// 工况内力链表
        /// </summary>
        public SortedList<string, ElemForce> LCForces
        {
            get { return _LCForces; }
            set { _LCForces = value; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public BElemForceTable()
        {
            _elem = 0;
            _LCForces = new SortedList<string, ElemForce>();
        }

        /// <summary>
        /// 给单元表添加工况内力
        /// </summary>
        /// <param name="lc">工况类型</param>
        /// <param name="force">工况内力</param>
        public void add_LCForce(string lc, ElemForce force)
        {
            _LCForces.Add(lc, force);
        }
        /// <summary>
        /// 判断是否含有相应的组合
        /// </summary>
        /// <param name="lc"></param>
        /// <returns></returns>
        public bool hasLC(string lc)
        {
            return _LCForces.ContainsKey(lc);
        }
    }
    #endregion

    #region 其它数据结构
    /// <summary>
    /// 结构组数据类[2010.8.10]
    /// </summary>
    [Serializable]
    public class BSGroup
    {
        #region 成员
        private string _GroupName;//组名称
        private List<int> _NodeList;//节点列表
        private List<int> _EleList;//单元列表
        #endregion

        #region 属性
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        /// <summary>
        /// 节点列表
        /// </summary>
        public List<int> NodeList
        {
            get { return _NodeList; }
        }
        /// <summary>
        /// 单元列表
        /// </summary>
        public List<int> EleList
        {
            get { return _EleList; }
        }
        #endregion
        #region 构造函数
        /// <summary>
        /// 无参数初始化
        /// </summary>
        public BSGroup()
        {
            _GroupName = null;
            _NodeList = new List<int>();
            _EleList = new List<int>();
        }
        /// <summary>
        /// 指定组名初始化
        /// </summary>
        /// <param name="Name">组名称</param>
        public BSGroup(string Name)
        {
            _GroupName = Name;
            _NodeList = new List<int>();
            _EleList = new List<int>();
        }
        #endregion
        #region 方法
        /// <summary>
        /// 添加节点列表入组
        /// </summary>
        /// <param name="NewList">节点列表</param>
        public void AddNodeList(List<int> NewList)
        {
            foreach (int NewNode in NewList)
            {
                if (_NodeList.Contains(NewNode))
                    continue;
                else
                {
                    _NodeList.Add(NewNode);
                }
            }

            _NodeList.Sort();//排序
        }

        /// <summary>
        /// 添加单元列表入组
        /// </summary>
        /// <param name="NewList">单元列表</param>
        public void AddElemList(List<int> NewList)
        {
            foreach (int NewElem in NewList)
            {
                if (_EleList.Contains(NewElem))
                    continue;
                else
                {
                    _EleList.Add(NewElem);
                }
            }

            _EleList.Sort();//排序
        }
        #endregion
    }
    #endregion

    /// <summary>
    /// 模型类：封装所有数据信息
    /// </summary>
    [Serializable]
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
        /// 荷载组合列表
        /// </summary>
         private BLoadCombTable _LoadCombTable;

        //荷载表
         private BLoadTable _LoadTable;
        /// <summary>
        /// 节点荷载链表
        /// </summary>
        public SortedList<int, BNLoad> conloads;

        /// <summary>
        /// 梁单元荷载链表
        /// </summary>
        public SortedList<int, BBLoad> beamloads;

        /// <summary>
        /// 单元温度荷载
        /// </summary>
        private SortedList<int, BETLoad> _EleTempLoads;

        /// <summary>
        /// 自重荷载信息链表
        /// </summary>
        public SortedList<string, BWeight> selfweight;

        /// <summary>
        /// 材料信息链表
        /// </summary>
        public SortedList<int, BMaterial> mats;

        /// <summary>
        /// 单元内力链表
        /// </summary>
        public SortedList<int, BElemForceTable> elemforce;

        private SortedList<string, BSGroup> _StruGroups;//结构组链表
        #endregion

        #region 属性
        /// <summary>
        /// 荷载组合字典
        /// </summary>
        public BLoadCombTable LoadCombTable
        {
            get { return _LoadCombTable; }
        }
        /// <summary>
        /// 荷载表字典
        /// </summary>
        public BLoadTable LoadTable
        {
            get { return _LoadTable; }
        }
        /// <summary>
        /// 组构组链表
        /// </summary>
        public SortedList<string, BSGroup> StruGroups
        {
            get { return _StruGroups; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
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
            _LoadCombTable = new BLoadCombTable();//荷载组合表
            _LoadTable = new BLoadTable();//所有荷载表
            conloads = new SortedList<int, BNLoad>(new RepeatedKeySort());//节点荷载
            beamloads = new SortedList<int, BBLoad>(new RepeatedKeySort());//梁单元荷载
            _EleTempLoads = new SortedList<int, BETLoad>(new RepeatedKeySort());//单元温度荷载
            selfweight = new SortedList<string, BWeight>();//自重信息
            mats = new SortedList<int, BMaterial>();//材料信息

            elemforce = new SortedList<int, BElemForceTable>();//单元内力表

            _StruGroups = new SortedList<string, BSGroup>();//结构组表
        }
        #endregion

        #region 模型处理方法
        /// <summary>
        /// 转化梁单元关键点信息
        /// </summary>
        public void GenBeamKpoint()
        {
            int Nnodes = 99999;//模型节点数基数，用于方向点的起始编号
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

            RtwMatrix m = new RtwMatrix(3, 3);
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

        /// <summary>
        /// 刷新单元局部坐标系
        /// 2010.05.25
        /// </summary>
        public void RefreshESC()
        {
            foreach (Element elee in this.elements.Values)
            {
                if (elee is FrameElement)//梁单元
                {
                    CoordinateSystem cs = new CoordinateSystem();
                    FrameElement ele = elee as FrameElement;
                    int iNum=ele.iNs[0];
                    int jNum=ele.iNs[1];

                    Point3d pti = new Point3d(nodes[iNum].X, nodes[iNum].Y, nodes[iNum].Z);
                    Point3d ptj = new Point3d(nodes[jNum].X, nodes[jNum].Y, nodes[jNum].Z);
                    Vector3 Vecx = pti.GetVectorTo(ptj);
                    Vecx.Normalize();//归一化
                    Vector3 Vz = new Vector3();//方向向量
                    if (Vecx.X == 0 && Vecx.Y == 0)
                    {
                        Vz = Vector3.xAxis;
                    }
                    else
                    {
                        Vz = Vector3.zAxis;
                    }

                    //方向向量按β角旋转
                    RtwMatrix Mz = MatrixFactory.GetRotationMartrix(Vecx, ele.beta) * Vz.ToMatrix();
                    Vz = new Vector3(Mz[0, 0], Mz[1, 0], Mz[2, 0]);//更新旋转后的结果
                    Vector3 Vy = Vz.CrossProduct(Vecx);
                    Vy.Normalize();//归一化

                    cs.Origin = pti;
                    cs.AxisX = Vecx;
                    cs.AxisY = Vy;
                    ele.ECS = cs;//更新单元坐标系
                }
                else if (elee is PlanarElement)//平面单元
                {
                    //to do:
                }
            }
        }

        /// <summary>
        ///对模型数据进行标准化处理
        /// </summary>
        public void Normalize()
        {
            //截面类型标准化
            foreach (BSections sec in sections.Values)
            {
                //解决箱形截面当用户没有输入tf2时对截面数据进行标准化
                if (sec is SectionDBuser)
                {
                    if (sec.SSHAPE == SecShape.B && (double)sec.SEC_Data[6] == 0
                    && (double)sec.SEC_Data[4] != 0)
                    {
                        sec.SEC_Data[6] = sec.SEC_Data[4];
                    }
                    //解决槽钢截面当用户没有输入tf2时对截面数据进行标准化
                    else if (sec.SSHAPE == SecShape.C && (double)sec.SEC_Data[5] == 0
                        && (double)sec.SEC_Data[2] != 0)
                    {
                        sec.SEC_Data[5] = sec.SEC_Data[2];
                        sec.SEC_Data[6] = sec.SEC_Data[4];
                    }
                }
                sec.CalculateSecProp();//计算截面特性
                //todo:当输入截面为数据库截面时，进行截面参数转化
            }

            //标准化材料特性
            foreach (BMaterial mat in mats.Values)
            {
                mat.NormalizeProp();
            }

            //更新单元局部坐标系
            RefreshESC();

            //更新最新的荷载表数据
            _LoadTable.UpdateNodeLoadList(this.STLDCASE, this.conloads);
        }

        /// <summary>
        /// 添加荷载组合入模型
        /// </summary>
        /// <param name="com"></param>
        public void AddLoadComb(BLoadComb com)
        {
            _LoadCombTable.Add(com);
        }

        /// <summary>
        /// 计算指定单元的单元组合内力
        /// </summary>
        /// <param name="com">组合名称</param>
        /// <param name="iElem">单元号</param>
        /// <returns>单元内力</returns>
        public  ElemForce CalElemForceComb(BLoadComb com, int iElem)
        {
            ElemForce res = new ElemForce();//要返回的结果

            List<BLCFactGroup> comdata = com.LoadCombData;            
            if (com.iTYPE == 0)//如果为线性组合
            {
                foreach (BLCFactGroup lfg in comdata)
                {
                    ElemForce ef = new ElemForce();
                    if (lfg.ANAL == ANAL.CB||lfg.ANAL==ANAL.CBS)
                    {
                        ef = this.CalElemForceComb(_LoadCombTable[lfg.LCNAME], iElem);//迭归组合
                    }
                    else if (lfg.ANAL == ANAL.RS)
                    {
                        ef = this.elemforce[iElem].LCForces[lfg.LCNAME + "(RS)"];
                    }
                    else
                    {
                        ef = elemforce[iElem].LCForces[lfg.LCNAME];//当前组合单元力
                    }
                    res=res+ef.Mutiplyby(lfg.FACT);
                }
            }
            else if (com.iTYPE == 1)//如果为+SRSS
            {

            }
            else if (com.iTYPE == 2)//如果为-SRSS
            {

            }
            else if (com.iTYPE==3)//如果为平方开根号
            {
                foreach (BLCFactGroup lfg in comdata)
                {
                    ElemForce ef = new ElemForce();
                    if (lfg.ANAL == ANAL.CB||lfg.ANAL==ANAL.CBS)
                    {
                        ef = this.CalElemForceComb(_LoadCombTable[lfg.LCNAME], iElem);//迭归组合
                    }
                    else if (lfg.ANAL == ANAL.RS)
                    {
                        ef = this.elemforce[iElem].LCForces[lfg.LCNAME + "(RS)"];
                    }
                    else
                    {
                        ef = elemforce[iElem].LCForces[lfg.LCNAME];//当前组合单元力
                    }
                    res = res + (ef.Mutiplyby(lfg.FACT)).Pow(2);//平方和
                }
                res = res.Pow(0.5);//开根号
            }
            return res;
        }

        /// <summary>
        /// 清理所有模型数据
        /// </summary>
        public void Reset()
        {
            unit = new BUNIT();

            nodes = new SortedList<int, Bnodes>();
            elements = new SortedList<int, Element>();
            sections = new SortedList<int, BSections>();
            thickness = new SortedList<int, BThickness>();

            constraint = new List<BConstraint>();

            STLDCASE = new List<BLoadCase>();
            _LoadCombTable = new BLoadCombTable();//荷载组合表
            conloads = new SortedList<int, BNLoad>(new RepeatedKeySort());//节点荷载
            beamloads = new SortedList<int, BBLoad>(new RepeatedKeySort());//梁单元荷载
            selfweight = new SortedList<string, BWeight>();//自重信息
            mats = new SortedList<int, BMaterial>();//材料信息

            elemforce = new SortedList<int, BElemForceTable>();//单元内力表
        }

        /// <summary>
        /// 取得线单元的长度
        /// </summary>
        /// <param name="iEle">线单元号</param>
        /// <returns></returns>
        public double getFrameLength(int iEle)
        {
            double res = 0;

            if (this.elements[iEle] is FrameElement)
            {
                FrameElement ele=this.elements[iEle] as FrameElement;
                res = this.nodes[ele.iNs[0]].DistanceTo(this.nodes[ele.iNs[1]]);
            }
            return res;
        }

        /// <summary>
        /// 取得线单元的方向向量（单位向量）：由I到J
        /// </summary>
        /// <param name="iEle">单元号</param>
        /// <returns>单位方向向量</returns>
        public Vector3 getFrameVec(int iEle)
        {
            Vector3 Res = new Vector3();
            if (this.elements[iEle] is FrameElement)
            {
                FrameElement fme = this.elements[iEle] as FrameElement;
                Res = this.nodes[fme.I].VectorTo(this.nodes[fme.J]);
            }
            return Res;
        }

        /// <summary>
        /// 由截面号返回单元号列表
        /// </summary>
        /// <param name="iSec">截面号</param>
        /// <returns>单元号列表</returns>
        public List<int> getElemBySec(int iSec)
        {
            List<int> Res = new List<int>();
            foreach (Element ele in this.elements.Values)
            {
                if (ele.iPRO == iSec&&ele is FrameElement)
                {
                    Res.Add(ele.iEL);
                }
            }
            return Res;
        }

        /// <summary>
        /// 地震反应谱组合和静力组合激活相互切换
        /// </summary>
        /// <param name="bActive">是否激活地震组合</param>
        public void RSCombineActive(bool bActive)
        {
            List<string> coms = LoadCombTable.ComSteel;//钢结构设计组合
            if (bActive == true)
            {//激活地震组合
                foreach (string com in coms)
                {
                    BLoadComb lc=LoadCombTable[com];
                    if (lc.hasLC_ANAL(ANAL.RS) || lc.hasLC_ANAL(ANAL.ES))
                    {
                        LoadCombTable[com].bACTIVE = true;
                    }
                    else
                    {
                        LoadCombTable[com].bACTIVE = false;
                    }
                }
            }
            else
            {//激活非地震组合
                foreach (string com in coms)
                {
                    BLoadComb lc = LoadCombTable[com];
                    if (lc.hasLC_ANAL(ANAL.RS) || lc.hasLC_ANAL(ANAL.ES))
                    {
                        LoadCombTable[com].bACTIVE = false;
                    }
                    else
                    {
                        LoadCombTable[com].bACTIVE = true;
                    }
                }
            }
        }
        #endregion

        #region model类输入输出接口方法
        /// <summary>
        /// 读取mgt文件信息
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        public void ReadFromMgt(string FilePath)
        {
            string currentdata = "notype";//指定当前数据类型
            string curLoadCase = "notype";//指定当前荷载工况

            int curSecNUM = 0;//当前截面号
            string curSecPOLY = null;//指示当前截面点对
            int IPOLY_num=0;//指示当前截面的内轮廓线数量

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

                        ElemType et = (ElemType)Enum.Parse(typeof(ElemType), temp[1].Trim(), true);

                        switch (et)
                        {
                            case ElemType.BEAM:
                                tempDoublt1 = double.Parse(temp[6], System.Globalization.NumberStyles.Float);
                                FrameElement elemdata = new FrameElement(
                                    tempInt, et, tempInt1, tempInt2, tempInt3, tempInt4);
                                if (Math.Abs(tempDoublt1) <= 0.0001)//如果读入角度非常小，近似认为方向角为0
                                    tempDoublt1 = 0;
                                elemdata.beta = tempDoublt1;//记录单元方向角
                                elements.Add(tempInt, elemdata);
                                break;
                            case ElemType.TRUSS:
                                goto case ElemType.BEAM;
                            case ElemType.PLATE:
                                tempInt5 = int.Parse(temp[6], System.Globalization.NumberStyles.Integer);
                                tempInt6 = int.Parse(temp[7], System.Globalization.NumberStyles.Integer);
                                tempInt7 = int.Parse(temp[8], System.Globalization.NumberStyles.Integer);
                                PlanarElement elemdata_P = new PlanarElement(
                                    tempInt, et, tempInt1, tempInt2, tempInt3, tempInt4, tempInt5, tempInt6);
                                elemdata_P.iSUB = tempInt7;
                                elements.Add(tempInt, elemdata_P);
                                break;
                            case ElemType.TENSTR:
                                tempDoublt2 = double.Parse(temp[6], System.Globalization.NumberStyles.Number);
                                tempInt5 = int.Parse(temp[7], System.Globalization.NumberStyles.Integer);
                                tempDoublt3 = double.Parse(temp[8], System.Globalization.NumberStyles.Integer);
                                FrameElement elemdata_T = new FrameElement(
                                    tempInt, et, tempInt1, tempInt2, tempInt3, tempInt4);
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
                #region 材料信息读取
                else if (line.StartsWith(" ") && currentdata == "*MATERIAL")
                {
                    //进行材料信息的读取
                    temp = line.Split(',');
                    try
                    {
                        tempInt = int.Parse(temp[0], System.Globalization.NumberStyles.Integer);//材料编号
                        MatType tp = (MatType )Enum.Parse(typeof(MatType), temp[1].Trim(), true);//材料类型
                        BMaterial mat = new BMaterial(tempInt,tp,temp[2].Trim());
                        mat.addMGTdata(line);//存储原始数据

                        switch (tp)
                        {
                            case MatType .STEEL:
                                mat.setProp(2.06e11,0.3,1.2e-5,7850);
                                break;
                            case MatType .CONC:
                                mat.setProp(3e10,0.2,1e-5,2500);//目前按C30输入
                                break;
                            case MatType .USER:
                                //mat.setProp(2.06e11, 0.3, 1.2e-5, 7850);
                                break;
                            default:
                                break;
                        }

                        mats.Add(tempInt, mat);//存储数据
                    }
                    catch
                    {
                        MessageBox.Show("解析材料信息出错！\n你的MIDAS模型用的什么鬼材料？？", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
                #region 截面数据读取
                else if (line.StartsWith(" ") && currentdata == "*SECTION")//一般截面
                {
                    //进行截面属性读取
                    temp = line.Split(',');
                    try
                    {
                        tempInt = int.Parse(temp[0], System.Globalization.NumberStyles.Number);//截面编号
                        SecType tt = (SecType)Enum.Parse(typeof(SecType), temp[1].Trim(),true);//截面类型(枚举解析)     
                  
                        #region 分类进行截面数据读取 
                        switch (tt)
                        {
                            case SecType.DBUSER:
                                BSections secdata = new SectionDBuser();
                                secdata.Num = tempInt;
                                secdata.TYPE = tt;//截面类型(枚举解析)                        
                                secdata.Name = temp[2].Trim();//截面名称

                                secdata.OFFSET[0] = temp[3];//截面偏心
                                for (int i = 1; i < 7; i++)
                                    secdata.OFFSET[i] = double.Parse(temp[i + 3], System.Globalization.NumberStyles.Number);

                                secdata.bsd = temp[10].Trim() == "YES" ? true : false;
                                secdata.SSHAPE = (SecShape)Enum.Parse(typeof (SecShape), temp[11].Trim(),true);//截面形状
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
                                break;
                            case SecType.TAPERED:
                                SectionTapered secTapered=new SectionTapered ();
                                secTapered.Num = tempInt;
                                secTapered.TYPE = tt;//截面类型(枚举解析)                        
                                secTapered.Name = temp[2].Trim();//截面名称
                                secTapered.OFFSET.Clear();
                                secTapered.OFFSET.Add(temp[3].Trim());//截面偏心
                                for (int i = 1; i < 9; i++)
                                    secTapered.OFFSET.Add(double.Parse(temp[i + 3], System.Globalization.NumberStyles.Number));

                                secTapered.bsd = temp[12].Trim() == "YES" ? true : false;
                                secTapered.SSHAPE =(SecShape) Enum.Parse(typeof (SecShape), temp[13].Trim(),true);//截面形状符号
                                secTapered.iyVAR =(iVAR)Enum.Parse(typeof(iVAR),temp[14],true);
                                secTapered.izVAR =(iVAR)Enum.Parse(typeof(iVAR), temp[15], true);
                                secTapered.SubTYPE = (STYPE)Enum.Parse(typeof(STYPE), temp[16], true);

                                line = reader.ReadLine();
                                secTapered.SEC_Data.Clear();
                                secTapered.SEC_Data.Add(line.Trim());

                                sections.Add(tempInt, secTapered);//存储截面
                                break;
                            default:
                                break;
                        }
                        #endregion
                        
                    }
                    catch
                    {
                        MessageBox.Show("解析常规截面属性出错！\n是否选用了不支持的截面数据类型？？", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #region 自定义截面读取
                else if (line.StartsWith(" ") && currentdata == "*SECT-GENERAL")//自定义截面
                {
                    if (line.Contains("SECT="))
                    {
                        SectionGeneral secGEN = new SectionGeneral();
                        temp = line.Trim().Remove(0, 5).Split(',');
                        tempInt = Convert.ToInt16(temp[0].Trim());//截面编号
                        SecType tt = (SecType)Enum.Parse(typeof(SecType), temp[1].Trim(), true);//截面类型(枚举解析)
                        secGEN.Num = tempInt;
                        curSecNUM = tempInt;//指定当前截面号，用于截面其它信息读取
                        curSecPOLY = null;//指定当前截面轮廓符号
                        IPOLY_num = 0;//指针指向零
                        secGEN.TYPE = tt;
                        secGEN.Name = temp[2].Trim();
                        secGEN.OFFSET.Add(temp[3].Trim());
                        for (int i = 1; i < 7; i++)
                            secGEN.OFFSET.Add( double.Parse(temp[i + 3], System.Globalization.NumberStyles.Number));

                        secGEN.bsd = temp[10].Trim() == "YES" ? true : false;
                        secGEN.SSHAPE = (SecShape)Enum.Parse(typeof(SecShape), temp[11].Trim(), true);//截面形状符号
                        secGEN.bBU = temp[12].Trim() == "YES" ? true : false;
                        secGEN.bEQ = temp[13].Trim() == "YES" ? true : false;

                        line = reader.ReadLine();//第二行
                        temp = line.Split(',');
                        secGEN.setSecProp1(double.Parse(temp[0]),double .Parse(temp[1]),double.Parse(temp[2]),
                            double .Parse(temp[3]),double.Parse(temp[4]),double.Parse(temp[5]));
                        line = reader.ReadLine();//第三行
                        temp = line.Split(',');
                        secGEN.setSecProp2(double.Parse(temp[0]), double.Parse(temp[1]), double.Parse(temp[2]),
                            double.Parse(temp[3]), double.Parse(temp[4]), double.Parse(temp[5]),
                            double.Parse(temp[6]), double.Parse(temp[7]), double.Parse(temp[8]),
                            double.Parse(temp[9]));
                        line = reader.ReadLine();//第四行
                        temp = line.Split(',');
                        secGEN.setSecProp3(double.Parse(temp[0]), double.Parse(temp[1]), double.Parse(temp[2]),
                            double.Parse(temp[3]), double.Parse(temp[4]), double.Parse(temp[5]),
                            double.Parse(temp[6]),double.Parse(temp[7]));

                        sections.Add(tempInt,secGEN);//将截面添加入模型对象
                    }
                    else if (line.Contains("OPOLY="))//含有OPOLY=的行
                    {
                        curSecPOLY = "OPOLY";//指示当前点对符号
                        temp = line.Trim().Remove(0, 6).Split(',');
                        if (sections[curSecNUM] is SectionGeneral)
                        {
                            SectionGeneral SGtemp = sections[curSecNUM] as SectionGeneral;
                            for (int i = 0; i < temp.Length; i = i + 2)
                            {
                                double d1 = Convert.ToDouble(temp[i]);
                                double d2 = Convert.ToDouble(temp[i + 1]);
                                SGtemp.addtoOPOLY(new Point2d(d1, d2));
                            }
                            sections.Remove(curSecNUM);//删除现有截面
                            sections.Add(curSecNUM, SGtemp);//增加新的截面
                        }
                    }
                    else if (line.Contains("IPOLY="))//含有IPOLY=的行
                    {
                        curSecPOLY = "IPOLY";//指示当前点对符号
                        IPOLY_num++;
                        temp = line.Trim().Remove(0, 6).Split(',');
                        if (sections[curSecNUM] is SectionGeneral)
                        {
                            SectionGeneral SGtemp = sections[curSecNUM] as SectionGeneral;
                            for (int i = 0; i < temp.Length; i = i + 2)
                            {
                                double d1 = Convert.ToDouble(temp[i]);
                                double d2 = Convert.ToDouble(temp[i + 1]);
                                SGtemp.addtoIPOLY(IPOLY_num - 1, new Point2d(d1, d2));
                            }
                            sections.Remove(curSecNUM);//删除现有截面
                            sections.Add(curSecNUM, SGtemp);//增加新的截面
                        }
                    }
                    else if (curSecPOLY == "OPOLY")//不含有OPOLY=的行
                    {
                        temp = line.Trim().Split(',');
                        if (sections[curSecNUM] is SectionGeneral)
                        {
                            SectionGeneral SGtemp = sections[curSecNUM] as SectionGeneral;
                            for (int i = 0; i < temp.Length; i = i + 2)
                            {
                                double d1 = Convert.ToDouble(temp[i]);
                                double d2 = Convert.ToDouble(temp[i + 1]);
                                SGtemp.addtoOPOLY(new Point2d(d1, d2));
                            }
                            sections.Remove(curSecNUM);//删除现有截面
                            sections.Add(curSecNUM, SGtemp);//增加新的截面
                        }
                    }
                    else if (curSecPOLY=="IPOLY")//不含有IPOLY=的行
                    {
                        temp = line.Trim().Split(',');
                        if (sections[curSecNUM] is SectionGeneral)
                        {
                            SectionGeneral SGtemp = sections[curSecNUM] as SectionGeneral;
                            for (int i = 0; i < temp.Length; i = i + 2)
                            {
                                double d1 = Convert.ToDouble(temp[i]);
                                double d2 = Convert.ToDouble(temp[i + 1]);
                                SGtemp.addtoIPOLY(IPOLY_num - 1, new Point2d(d1, d2));
                            }
                            sections.Remove(curSecNUM);//删除现有截面
                            sections.Add(curSecNUM, SGtemp);//增加新的截面
                        }
                    }
                }
                #endregion
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
                        lcdata.LCName = temp[0].Trim();

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
                        BeamLoadData.TYPE = (BeamLoadType)Enum.Parse(typeof(BeamLoadType), temp1[2].Trim());//荷载类型
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

                #region 温度荷载读取
                else if (line.StartsWith(" ") == true && currentdata == "*ELTEMPER")
                {
                    //拆分字符
                    temp = curLoadCase.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);//temp[1]为工况名
                    temp1 = line.Split(new char[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries);//temp1为数据组
                    try
                    {
                        BETLoad TempLoad = new BETLoad();
                        TempLoad.Elem_Num = Convert.ToInt32( temp1[0].Trim());//单元号
                        TempLoad.Temp = Convert.ToDouble(temp1[1].Trim());//单元温度
                        TempLoad.LC = temp[1];//工况
                        if (temp1[2] != " ")
                        {
                             TempLoad.Group = temp1[2].Trim();//装入组名
                        }
                        _EleTempLoads.Add(TempLoad.Elem_Num, TempLoad);//加入模型数据库
                    }
                    catch
                    {
                        MessageBox.Show("解析单元温度荷载表出错！\n", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
            }
            reader.Close();
            #region 再次打开文件并读取荷载组合
            FileStream str = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(str,Encoding.Default);//用系统默认编码打开
            ReadLoadComb(ref sr);   //读取荷载组合
            sr.Close();
            #endregion

            #region 再次打开文件并读取结构组
            str = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            sr = new StreamReader(str, Encoding.Default);//用系统默认编码打开
            ReadStruGroups(ref sr); //读取结构组
            sr.Close();
            #endregion

            Normalize();//模型标准化处理
            GenBeamKpoint();//计算模型中梁单元的节点方向点信息
        }

        /// <summary>
        /// 读取荷载组合列表
        /// </summary>
        /// <param name="srt">文件流</param>
        public void ReadLoadComb(ref StreamReader srt)
        {
            /* 1、准备*/
            bool bRead = false;                     //是否可以读取
            String strText = null;                  //当前行文本
            String strStartFlag = "*LOADCOMB";      //数据开始标志
            String strEndFlag = "";                 //数据结束标志
            char szSplit = ',';                     //数据分隔符

            /* 2、循环读取*/
            string curName = null;                  //当前荷载组合名称
            for (strText = srt.ReadLine(); strText != null; strText = srt.ReadLine())
            {
                /* 2.1、判断是否读到数据。若读到，设置标志，进入下一轮循环开始读取；若没有读到，继续进入下一轮判断。*/
                /* 2.2、bRead=true，表示已经可以读数据了。读的时候要判断是否已经读完数据。*/
                if (!bRead)
                {
                    if (strText.StartsWith(strStartFlag))
                    {
                        bRead = true;
                    }
                    continue;
                }
                else if (strText.StartsWith(";"))//如果为注释则忽略
                    continue;
                else if (strText.CompareTo(strEndFlag) == 0)
                    return;
                else if (strText.Trim().StartsWith("NAME"))
                {   
                    /*进入当前荷载组合基本数据读取*/
                    string[] sArrayCur=strText.Trim().Split(szSplit);
                    string sName=sArrayCur[0].Substring(sArrayCur[0].IndexOf('=')+1).Trim();//组合名称
                    LCKind kind=LCKind.GEN;//组合类型
                    bool isActive=true;//是否激活
                    bool bEs=false;
                    switch(sArrayCur[1].Trim())
                    {
                        case "GEN":kind =LCKind.GEN;break ;
                        case "STEEL":kind =LCKind.STEEL;break ;
                        case "CONC":kind=LCKind.CONC;break;
                        case "SRC":kind =LCKind.SRC;break;
                        case "FDN":kind =LCKind.FDN;break;
                        default:kind =LCKind.GEN;break;
                    }
                    if (sArrayCur[2].Trim()=="INACTIVE")
                    {
                        isActive=false;
                    }
                    if (sArrayCur[3].Trim()!="0")
                        bEs=true;
                    int Type=Convert.ToInt16(sArrayCur[4].Trim());
                    BLoadComb blc = new BLoadComb();        //当前荷载组合数据
                    blc.SetData1(sName, kind, isActive, bEs, Type, sArrayCur[5].Trim());
                    curName = sName;//记录当前名称
                    _LoadCombTable.Add(blc);
                    continue;
                }
                else if (strText.StartsWith(" ")&&strText.Contains(szSplit.ToString()))
                {
                    /*进入当前荷载组合工况对添加*/
                    //BLoadComb tempBLC = LOADCOMBS[curName];
                    BLoadComb tempBLC = _LoadCombTable[curName];//取出当前组合
                    //LOADCOMBS.Remove(curName);
                    _LoadCombTable.Remove(curName);//从组合表中删除
                    string[] sArrayCur = strText.Trim().Split(szSplit);                  
                    for (int i = 0; i < sArrayCur.Length; i=i+3)
                    {
                        BLCFactGroup lcfg = new BLCFactGroup();
                        switch (sArrayCur[i].Trim())
                        {
                            case "TH": lcfg.ANAL=ANAL.TH; break;
                            case "SM": lcfg.ANAL = ANAL.SM; break;
                            case "RS": lcfg.ANAL = ANAL.RS; break;
                            case "MV": lcfg.ANAL = ANAL.MV; break;
                            case "ST": lcfg.ANAL = ANAL.ST; break;
                            case "CB": lcfg.ANAL = ANAL.CB; break;
                            case "CBS": lcfg.ANAL = ANAL.CBS; break;
                            case "ES": lcfg.ANAL = ANAL.ES; break;
                            default: lcfg.ANAL = ANAL.ST; break;
                        }
                        lcfg.LCNAME = sArrayCur[i + 1].Trim();
                        lcfg.FACT = Convert.ToDouble(sArrayCur[i + 2]);
                        tempBLC.AddLCFactGroup(lcfg);                        
                    }
                    _LoadCombTable.Add(tempBLC);//再添加到组合表中
                    //_LoadCombs.Add(tempBLC.NAME, tempBLC);
                }
                else
                    continue;
            }
        }

        /// <summary>
        /// 读取结构组数据表
        /// </summary>
        /// <param name="str">文件流</param>
        public void ReadStruGroups(ref StreamReader srt)
        {
            /* 1、准备*/
            bool bRead = false;                     //是否可以读取
            String strText = null;                  //当前行文本
            String strStartFlag = "*GROUP";      //数据开始标志
            String strEndFlag = "";                 //数据结束标志
            char szSplit = ',';                     //数据分隔符
            int iGroupFlag = 0;                  //组数据标志：0-新组,1-节点数据,2-单元数据

            /* 2、循环读取*/
            BSGroup Group = null;                   //组
            for (strText = srt.ReadLine(); strText != null; strText = srt.ReadLine())
            {
                /* 2.1、判断是否读到数据。若读到，设置标志，进入下一轮循环开始读取；若没有读到，继续进入下一轮判断。*/
                /* 2.2、bRead=true，表示已经可以读数据了。读的时候要判断是否已经读完数据。*/
                if (!bRead)
                {
                    if (strText.StartsWith(strStartFlag))
                    {
                        bRead = true;
                    }
                    continue;
                }
                else if (strText.StartsWith(";"))//如果为注释则忽略
                    continue;
                else if (strText.CompareTo(strEndFlag) == 0)//如果读取数据结尾则返回
                    return;
                else
                {
                    #region 读取数据
                    string[] sCurs = strText.Trim().Split(szSplit);
                    if (sCurs.Length == 3 && iGroupFlag == 0)//第一行有两个分隔符时
                    {
                        Group = new BSGroup(sCurs[0].Trim());//创建新组
                        List<int> nodes = Tools.SelectCollection.StringToList(sCurs[1].Trim());
                        List<int> elems = new List<int>();
                        if (sCurs[2].EndsWith("\\"))
                        {
                            iGroupFlag = 2;      //指定下一行为旧数据
                            elems = Tools.SelectCollection.StringToList(sCurs[2].TrimEnd('\\'));
                        }
                        else
                        {
                            iGroupFlag = 0;     //指定下一行为新组
                            elems = Tools.SelectCollection.StringToList(sCurs[2]);
                        }
                        Group.AddNodeList(nodes);//添加节点表
                        Group.AddElemList(elems);//添加单元表

                        //添加入模型数据库
                        if (strText.EndsWith("\\") == false)
                        {
                            this.StruGroups.Add(Group.GroupName, Group);
                        }
                    }
                    else if (sCurs.Length == 2 && iGroupFlag == 0)//第一行有一个分隔符时
                    {
                        Group = new BSGroup(sCurs[0].Trim());//创建新组
                        List<int> nodes = new List<int>();
                        if (sCurs[1].EndsWith("\\"))
                        {
                            iGroupFlag = 1;      //指定下一行为旧数据
                            nodes = Tools.SelectCollection.StringToList(sCurs[1].TrimEnd('\\'));
                        }
                        else
                        {
                            iGroupFlag = 0;     //指定下一行为新组
                            nodes = Tools.SelectCollection.StringToList(sCurs[1]);
                        }
                        Group.AddNodeList(nodes);//添加节点表

                        //添加入模型数据库
                        if (strText.EndsWith("\\") == false)
                        {
                            this.StruGroups.Add(Group.GroupName, Group);
                        }
                    }
                    else if (sCurs.Length == 1 && iGroupFlag == 1)//节点数据行
                    {
                        List<int> nodes = new List<int>();
                        string temp = strText;
                        if (strText.EndsWith("\\") == false)
                        {
                            iGroupFlag = 0;//新的组
                        }
                        else
                        {
                            temp = temp.TrimEnd('\\');
                        }
                        nodes = Tools.SelectCollection.StringToList(temp);

                        Group.AddNodeList(nodes);

                        //添加入模型数据库
                        if (strText.EndsWith("\\") == false)
                        {
                            this.StruGroups.Add(Group.GroupName, Group);
                        }
                    }
                    else if (sCurs.Length == 1 && iGroupFlag == 2)//单元数据行
                    {
                        List<int> elems = new List<int>();
                        string temp = strText;
                        if (strText.EndsWith("\\") == false)
                        {
                            iGroupFlag = 0;//新的组
                        }
                        else
                        {
                            temp = temp.TrimEnd('\\');
                        }
                        elems = Tools.SelectCollection.StringToList(temp);

                        Group.AddElemList(elems);

                        //添加入模型数据库
                        if (strText.EndsWith("\\") == false)
                        {
                            this.StruGroups.Add(Group.GroupName, Group);
                        }
                    }
                    else if (sCurs.Length == 2 && iGroupFlag == 1)//同时有节点数据和单元数据
                    {
                        List<int> nodes = new List<int>();
                        List<int> elems = new List<int>();
                        string temp1 = sCurs[0];
                        string temp2 = sCurs[1];
                        if (temp2.EndsWith("\\") == false)
                        {
                            iGroupFlag = 0;//新的组
                        }
                        else
                        {
                            temp2 = temp2.TrimEnd('\\');
                            iGroupFlag = 2;//更新数据标志
                        }

                        nodes = Tools.SelectCollection.StringToList(temp1);
                        elems = Tools.SelectCollection.StringToList(temp2);

                        Group.AddNodeList(nodes);
                        Group.AddElemList(elems);
          
                        //添加入模型数据库
                        if (strText.EndsWith("\\") == false)
                        {
                            this.StruGroups.Add(Group.GroupName, Group);
                        }
                    }
                    else
                        continue;
                    #endregion
                }
            }
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
            writer.WriteLine("/COM,Midas2Ansys INP File Created at " + System.DateTime.Now);
            writer.WriteLine("/COM,*******http://www.lubanren.com********");

            #region 宏定义
            writer.WriteLine("\n!SPC截面宏定义...并执行");
            foreach (KeyValuePair<int, BSections> sec in this.sections)
            {
                if (sec.Value is SectionGeneral)
                {
                    SectionGeneral secg = sec.Value as SectionGeneral;
                    writer.WriteLine(secg.GetSectMac());//取得宏
                    writer.WriteLine("*use," + secg.Name + ".sec" + "\t!执行宏");
                }
            }
            #endregion

            writer.WriteLine("FINISH");
            writer.WriteLine("/CLEAR");
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
                    writer.WriteLine("keyopt,1,1,1");
                    writer.WriteLine("keyopt,1,3,3");
                    //writer.WriteLine("keyopt,1,9,2");
                    break;
                case 3:
                    writer.WriteLine("et,1,189");
                    break;
                default:
                    //writer.WriteLine("et,1,188");
                    break;
            }
            //板单元类型声明
            writer.WriteLine("et,2,181");
            writer.WriteLine("keyopt,2,3,2");
            writer.WriteLine("keyopt,2,8,2");            
            //桁架、只受拉、只受压单元类型声明
            writer.WriteLine("et,3,180");

            //实常数信息
            writer.WriteLine("\n!实常数信息定义...");
            foreach (KeyValuePair<int, BThickness> thi in this.thickness)//遍历板厚信息形成实常数
            {
                writer.WriteLine("r,{0},{1}", thi.Key.ToString(), thi.Value.THIK_IN.ToString());
            }
            #region LINK180单元实常数处理定义
            List<int> TRU_sec = new List<int>();//临时记录变链表
            List <int> TEN_sec=new List<int> ();
            List <int> COM_sec=new List<int> ();
            foreach (KeyValuePair<int, Element> elem in this.elements)// 遍历单元信息形成实常数（link180单元用）
            {
                if (elem.Value.TYPE == ElemType.TRUSS)
                {
                    int num=elem.Value.iPRO;
                    if (!TRU_sec.Contains(num))
                        TRU_sec.Add(num);                    
                }
                else if (elem.Value.TYPE == ElemType.TENSTR)
                {
                    int num = elem.Value.iPRO;
                    if (!TEN_sec.Contains(num))
                        TEN_sec.Add(num);
                }
                else if (elem.Value.TYPE == ElemType.COMPTR)
                {
                    int num = elem.Value.iPRO;
                    if (!COM_sec.Contains(num))
                        COM_sec.Add(num);                   
                }
            }

            foreach (int tru in TRU_sec)
            {
                writer.WriteLine("r,{0},{1},,0", tru+100,this.sections[tru].Area.ToString("G3"));
            }
            foreach (int ten in TEN_sec)
            {
                writer.WriteLine("r,{0},{1},,1", ten + 200, this.sections[ten].Area.ToString("G3"));
            }
            foreach (int com in COM_sec)
            {
                writer.WriteLine("r,{0},{1},,-1", com + 300, this.sections[com].Area.ToString("G3"));
            }
            #endregion


            writer.WriteLine("\n!截面信息定义...");
            foreach (KeyValuePair<int, BSections> sec in this.sections)
            {
                writer.WriteLine(sec.Value.WriteData());
            }

            #region 材料信息输出
            writer.WriteLine("\n!材料信息定义...");
            foreach (KeyValuePair<int, BMaterial> mat in this.mats)
            {
                writer.WriteLine("mp,ex,{0},{1}", mat.Value.iMAT.ToString("G"), mat.Value.Elast.ToString("G"));
                writer.WriteLine("mp,prxy,{0},{1}", mat.Value.iMAT.ToString("G"), mat.Value.Poisn.ToString("G"));
                writer.WriteLine("mp,dens,{0},{1}", mat.Value.iMAT.ToString("G"), mat.Value.Den.ToString("G"));
                writer.WriteLine("mp,alpx,{0},{1}", mat.Value.iMAT.ToString("G"), mat.Value.Thermal.ToString("G"));
            }
            #endregion
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
                    case ElemType.BEAM:
                        writer.WriteLine("type,1");
                        writer.WriteLine("mat,{0}", elem.Value.iMAT.ToString());
                        writer.WriteLine("secnum," + elem.Value.iPRO.ToString());
                        writer.WriteLine("real,");
                        writer.WriteLine("en," + elem.Value.NodeString());
                        break;
                    case ElemType.TRUSS:
                        writer.WriteLine("type,3");
                        writer.WriteLine("mat,{0}", elem.Value.iMAT.ToString());
                        writer.WriteLine("real,{0}",elem.Value.iPRO+100);
                        writer.WriteLine("en,{0}",elem.Value.NodeString());
                        break;
                    case ElemType.PLATE:
                        //writer.WriteLine("!{0}号单元是平面单元", elem.Value.iEL.ToString());
                        writer.WriteLine("real,{0}", elem.Value.iPRO.ToString());//厚度号
                        writer.WriteLine("type,2");
                        writer.WriteLine("mat,{0}", elem.Value.iMAT.ToString());
                        writer.WriteLine("secnum");//将截面号置为初始值
                        writer.WriteLine("en," + elem.Value.NodeString());
                        break;
                    case ElemType.TENSTR:
                        //writer.WriteLine("!{0}号单元是只拉单元", elem.Value.iEL.ToString());
                        writer.WriteLine("type,3");
                        writer.WriteLine("mat,{0}", elem.Value.iMAT.ToString());
                        writer.WriteLine("real,{0}", elem.Value.iPRO + 200);
                        writer.WriteLine("en,{0}", elem.Value.NodeString());
                        break;
                    case ElemType.COMPTR:
                        //writer.WriteLine("!{0}号单元是只压单元", elem.Value.iEL.ToString());
                        writer.WriteLine("type,3");
                        writer.WriteLine("mat,{0}", elem.Value.iMAT.ToString());
                        writer.WriteLine("real,{0}", elem.Value.iPRO + 300);
                        writer.WriteLine("en,{0}", elem.Value.NodeString());
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
                        //输出单元荷载信息
                        if (bload.Value.TYPE == BeamLoadType.UNIMOMENT ||
                            bload.Value.TYPE == BeamLoadType.CONMOMENT ||
                            bload.Value.getP(3) != 0)
                        {
                            writer.WriteLine("!单元({0})荷载为弯矩荷载,在ANSYS中需要单元细化...", bload.Key.ToString());
                        }
                        else if (bload.Value.TYPE == BeamLoadType.UNILOAD)
                        {
                            switch (bload.Value.Dir)
                            {
                                case DIR.GX:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系为全局坐标系GX,未处理",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GY:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系为全局坐标系GY,未处理",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GZ:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系为全局坐标系GZ,未处理",
                                        bload.Key.ToString());
                                    break;
                                case DIR.LZ:
                                    writer.WriteLine("sfbeam,{0},1,pres,{1},{2},,,{3},{4},1",
                                        bload.Key.ToString(),
                                        (-bload.Value.getP(1)).ToString(),
                                        (-bload.Value.getP(2)).ToString(),
                                        bload.Value.getD(1).ToString(),
                                        bload.Value.getD(2).ToString());
                                    break;
                                case DIR.LY:
                                    writer.WriteLine("sfbeam,{0},2,pres,{1},{2},,,{3},{4},1",
                                        bload.Key.ToString(),
                                        (-bload.Value.getP(1)).ToString(),
                                        (-bload.Value.getP(2)).ToString(),
                                        bload.Value.getD(1).ToString(),
                                        bload.Value.getD(2).ToString());
                                    break;
                                case DIR.LX:
                                    writer.WriteLine("sfbeam,{0},3,pres,{1},{2},,,{3},{4},1",
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
                        else if (bload.Value.TYPE == BeamLoadType.CONLOAD && bload.Value.getP(2) == 0)
                        {
                            switch (bload.Value.Dir)
                            {
                                case DIR.GX:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系为全局坐标系GX,未处理",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GY:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系为全局坐标系GY,未处理",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GZ:
                                    writer.WriteLine("!单元({0})的单元荷载施加坐标系为全局坐标系GZ,未处理",
                                        bload.Key.ToString());
                                    FrameElement fe = elements[bload.Key] as FrameElement;
                                    CoordinateSystem cs = fe.ECS;
                                    double angel = Vector3.zAxis.Angle(cs.AxisZ);
                                    break;
                                case DIR.LZ:
                                    writer.WriteLine("sfbeam,{0},1,pres,{1},,,,{2},-1,1",
                                        bload.Key.ToString(),
                                        (-bload.Value.getP(1)).ToString(),
                                        bload.Value.getD(1).ToString());
                                    break;
                                case DIR.LY:
                                    writer.WriteLine("sfbeam,{0},2,pres,{1},,,,{2},-1,1",
                                        bload.Key.ToString(),
                                        (-bload.Value.getP(1)).ToString(),
                                        bload.Value.getD(1).ToString());
                                    break;
                                case DIR.LX:
                                    writer.WriteLine("sfbeam,{0},3,pres,{1},,,,{2},-1,1",
                                        bload.Key.ToString(),
                                        bload.Value.getP(1).ToString(),
                                        bload.Value.getD(1).ToString());
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                #endregion
                #region 输出单元温度荷载
                foreach (KeyValuePair<int, BETLoad> Eload in this._EleTempLoads)
                {
                    if (Eload.Value.LC != lc.LCName)
                        continue;
                    writer.WriteLine("bfe,{0},temp,1,{1}",
                        Eload.Value.Elem_Num,
                        Eload.Value.Temp.ToString());
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

        /// <summary>
        /// 写出ansys宏文件
        /// 功能：将荷载转化为质量
        /// </summary>
        /// <param name="dir">宏文件目录</param>
        /// <returns>是否成功</returns>
        public bool WriteAnsysMarc_Load2Mass(string dir)
        {
            string FileName=Path.Combine(dir,"loadtomass.mac");
            using (FileStream stream = File.Open(FileName, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);

                #region 宏文件内容
                writer.WriteLine("!宏文件功能：将荷载转化为质量");
                writer.WriteLine("/COM,Ansys Mac File Created at " + System.DateTime.Now);
                writer.WriteLine("/COM,*******http://www.lubanren.com********");

                writer.WriteLine("et,11,21");
                writer.WriteLine("keyopt,11,1,0");
                writer.WriteLine("keyopt,11,2,0");
                writer.WriteLine("keyopt,11,3,0");

                writer.WriteLine("!todo:通过实常数定义节点质量");
                writer.WriteLine("*get,enmax, elem,0, num, maxd");

                //节点荷载进行遍历
                BLoadTable blt = this.LoadTable;
                SortedList<int, BNLoad> NLoad_DL = blt.NLoadData["DL"] as SortedList<int, BNLoad>;
                SortedList<int, BNLoad> NLoad_LL = blt.NLoadData["LL"] as SortedList<int, BNLoad>;
                double DL_ra=1.0;//工况荷载系数
                double LL_ra=0.5;
                int i = 1;//单元数指示标志

                List<int> nodes = blt.NodeListForNLoad;//具有节点荷载的节点列表
                foreach (int nn in nodes)
                {
                    
                    if (NLoad_DL.ContainsKey(nn) || NLoad_LL.ContainsKey(nn))
                    {
                        double mass = 0;
                        if (NLoad_DL.ContainsKey(nn))
                        {
                            mass +=Math.Abs(NLoad_DL[nn].FZ) / 9.8 * DL_ra;
                        }
                        if (NLoad_LL.ContainsKey(nn))
                        {
                            mass += Math.Abs(NLoad_LL[nn].FZ) / 9.8 * LL_ra;
                        }
                        //实常数定义
                        writer.WriteLine("r,enmax+{0},{1},{1},{1},,,", i, mass.ToString("0.0"));

                        //单元定义
                        writer.WriteLine("real,enmax+{0}",i);
                        writer.WriteLine("type,11$mat$secnum");
                        writer.WriteLine("en,enmax+{0},{1}",i,nn);
                        i++;
                    }
                }

                //清理垃圾
                writer.WriteLine("enmax=");
                writer.WriteLine("real$type$mat$secnum");

                #endregion

                writer.Close();
                stream.Close();
            }

            return true;
        }

        /// <summary>
        /// 读取MIDAS输出的梁单元内力结果入模型
        /// </summary>
        /// <param name="MidasFile">MIDAS输出的单元内力表，单位默认为kN,m</param>
        public void ReadElemForces(string MidasForceFile)
        {
            string line = null;//行文本
            string[] curdata= null;//当前数据表存储变量
            int curNum=0;//当前单元号
            string curLC=null;//当前工况名
            double[] tempDouble =new double[6];

            int i = 0;
            
            FileStream stream = File.Open(MidasForceFile, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            line = reader.ReadLine();
            
            for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                curdata = line.Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);//字符串分割
                curNum = int.Parse(curdata[0], System.Globalization.NumberStyles.Number);//当前单元号
                curLC=curdata[1];//当前工况名称

                 //取得内力数据
                for (int k = 0; k < 6; k++)
                {
                    tempDouble[k] = double.Parse(curdata[k+3], System.Globalization.NumberStyles.Float);
                }
                //建立截面内力
                SecForce sec1 = new SecForce(tempDouble[0],tempDouble[3],tempDouble[1],
                    tempDouble[2],tempDouble[4],tempDouble[5]);

                #region 往模型数据结构中添加
                if (this.elemforce.ContainsKey(curNum))//如果已有当前单元
                {
                    if (this.elemforce[curNum].hasLC(curLC))//如果已有当前组合
                    {
                        SortedList<string,ElemForce> tempEF= this.elemforce[curNum].LCForces;
                        ;
                        if (curdata[2].StartsWith("I"))
                        {
                            tempEF[curLC].SetElemForce(sec1, 0);
                        }
                        else if (curdata[2].StartsWith("J"))
                        {
                            tempEF[curLC].SetElemForce(sec1, 8);
                        }
                        else if (curdata[2] == "1/4")
                        {
                            tempEF[curLC].SetElemForce(sec1, 2);
                        }
                        else if (curdata[2] == "2/4")
                        {
                            tempEF[curLC].SetElemForce(sec1, 4);
                        }
                        else if (curdata[2] == "3/4")
                        {
                            tempEF[curLC].SetElemForce(sec1, 6);
                        }

                        this.elemforce[curNum].LCForces = tempEF;//反回的到模型数据库中
                    }
                    else
                    {
                        ElemForce ef = new ElemForce();
                        if (curdata[2].StartsWith("I"))
                        {
                            ef.SetElemForce(sec1, 0);
                        }
                        else if (curdata[2].StartsWith("J"))
                        {
                            ef.SetElemForce(sec1, 8);
                        }
                        else if (curdata[2] == "1/4")
                        {
                            ef.SetElemForce(sec1, 2);
                        }
                        else if (curdata[2] == "2/4")
                        {
                            ef.SetElemForce(sec1, 4);
                        }
                        else if (curdata[2] == "3/4")
                        {
                            ef.SetElemForce(sec1, 6);
                        }
                        this.elemforce[curNum].add_LCForce(curLC, ef);
                    }
                }
                else
                {
                    ElemForce ef = new ElemForce();

                    if (curdata[2].StartsWith("I"))
                    {
                        ef.SetElemForce(sec1, 0);
                    }
                    else if (curdata[2].StartsWith("J"))
                    {
                        ef.SetElemForce(sec1, 8);
                    }
                    else if (curdata[2] == "1/4")
                    {
                        ef.SetElemForce(sec1, 2);
                    }
                    else if (curdata[2] == "2/4")
                    {
                        ef.SetElemForce(sec1, 4);
                    }
                    else if (curdata[2] == "3/4")
                    {
                        ef.SetElemForce(sec1, 6);
                    }

                    BElemForceTable eft=new BElemForceTable ();
                    eft.add_LCForce(curLC,ef);
                    this.elemforce.Add(curNum,eft);
                }
                #endregion
                i++;
            }
            reader.Close();
        }

        /// <summary>
        /// 读取Midas输出的Truss单元内力信息
        /// </summary>
        /// <param name="MidasTrussForceOut">桁架单元内力表，单位默认为kN</param>
        public void ReadTrussForces(string MidasTrussForceOut)
        {
            string line = null;//行文本
            string[] curdata = null;//当前数据表存储变量
            int curNum = 0;//当前单元号
            string curLC = null;//当前工况名
            double[] tempDouble = new double[2];

            int i = 0;

            FileStream stream = File.Open(MidasTrussForceOut, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            line = reader.ReadLine();

            for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                curdata = line.Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);//字符串分割
                curNum = int.Parse(curdata[0], System.Globalization.NumberStyles.Number);//当前单元号
                curLC = curdata[1];//当前工况名称
                //取得内力数据
                for (int k = 0; k < 2; k++)
                {
                    tempDouble[k] = double.Parse(curdata[k +2], System.Globalization.NumberStyles.Float);
                }

                //建立截面内力
                SecForce sec1 = new SecForce(tempDouble[0], 0, 0, 0, 0, 0);//I截面内力
                SecForce sec2 = new SecForce(tempDouble[1], 0, 0, 0, 0, 0);//J截面内力

                #region 往模型数据结构中添加
                if (this.elemforce.ContainsKey(curNum))//如果已有当前单元
                {
                    if (this.elemforce[curNum].hasLC(curLC))//如果已有当前组合
                    {
                        SortedList<string, ElemForce> tempEF = this.elemforce[curNum].LCForces;
                        tempEF[curLC].SetElemForce(sec1, sec2);
                        this.elemforce[curNum].LCForces = tempEF;//反回的到模型数据库中
                    }
                    else
                    {
                        ElemForce ef = new ElemForce();
                        ef.SetElemForce(sec1, sec2);
                        this.elemforce[curNum].add_LCForce(curLC, ef);
                    }
                }
                else
                {
                    ElemForce ef = new ElemForce();
                    ef.SetElemForce(sec1, sec2);
                    BElemForceTable eft = new BElemForceTable();
                    eft.add_LCForce(curLC, ef);
                    this.elemforce.Add(curNum, eft);
                }
                #endregion

                i++;
            }

            reader.Close();
        }
        #endregion
    }

    #region 常用基本数据类型
    /// <summary>
    /// 实现hash表重复键成员的添加
    /// </summary>
    [Serializable]
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
    /// <summary>
    /// 实现SortedList不自动排序功能
    /// </summary>
    [Serializable]
    public class NoAutoSort : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int iResult = string.Compare(x,y);
            if (iResult != 0)
                iResult = -1;
            return iResult;
            //排序
            // int iResult = (int)x - (int)y;
            // if(iResult == 0) iResult = -1;
            // return iResult;
        }
    }
    #endregion 
}
