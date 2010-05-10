using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MidasGenModel.Design;


namespace MidasGenModel.model
{
    #region Model Info(ģ��������)

    /// <summary>
    /// ģ�͵�λ��Ϣ
    /// </summary>
    [Serializable]
    public class BUNIT
    {
        private string _Force;
        private string _Length;
        private string _Heat;
        private string _Temper;

        /// <summary>
        /// ���ĵ�λ��N��KN��
        /// </summary>
        public string Force
        {
            set { _Force = value; }
            get { return _Force; }
        }

        /// <summary>
        /// ���ȵ�λ��m��mm��
        /// </summary>
        public string Length
        {
            set { _Length = value; }
            get { return _Length; }
        }

        /// <summary>
        /// ������λ��kJ��
        /// </summary>
        public string Heat
        {
            set { _Heat = value; }
            get { return _Heat; }
        }

        /// <summary>
        /// �¶ȵ�λ��C��
        /// </summary>
        public string Temper
        {
            set { _Temper = value; }
            get { return _Temper; }
        }

        /// <summary>
        /// Ĭ�Ϲ��캯��
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
    #region Load Class(������)

    /// <summary>
    /// ���ع�������
    /// </summary>
    public enum LCType
    {
        /// <summary>
        /// ���
        /// </summary>
        D,
        /// <summary>
        /// ���
        /// </summary>
        L,
        /// <summary>
        /// ���
        /// </summary>
        W,
        /// <summary>
        /// �������
        /// </summary>
        E,
        /// <summary>
        /// ������
        /// </summary>
        LR,
        /// <summary>
        /// ѩ��
        /// </summary>
        S,
        /// <summary>
        /// �¶Ⱥ���
        /// </summary>
        T,
        /// <summary>
        /// ԤӦ������
        /// </summary>
        PS,
        /// <summary>
        /// �û�����
        /// </summary>
        USER
    }

    /// <summary>
    /// ������ϵ�����
    /// </summary>
    public enum LCKind
    {
        /// <summary>
        /// General���
        /// </summary>
        GEN,
        /// <summary>
        /// �ֽṹ��������
        /// </summary>
        STEEL,
        /// <summary>
        /// ��������������
        /// </summary>
        CONC,
        /// <summary>
        /// SRC��������
        /// </summary>
        SRC,
        /// <summary>
        /// ������������
        /// </summary>
        FDN
    }

    /// <summary>
    /// ��λ������������
    /// </summary>
    public enum ANAL
    {
        /// <summary>
        /// Static ����
        /// </summary>
        ST,
        /// <summary>
        /// Response Spectrum ��Ӧ��
        /// </summary>
        RS,
        /// <summary>
        /// Time History ʱ��
        /// </summary>
        TH,
        /// <summary>
        /// Moving �ƶ�
        /// </summary>
        MV,
        /// <summary>
        /// Settlement ����
        /// </summary>
        SM,
        /// <summary>
        /// ���
        /// </summary>
        CB
    }
    /// <summary>
    ///���ط��� 
    /// </summary>
    public enum DIR
    {
        /// <summary>
        /// ��������X��
        /// </summary>
        GX,
        /// <summary>
        /// ��������Y��
        /// </summary>
        GY,
        /// <summary>
        /// ��������Z��
        /// </summary>
        GZ,
        /// <summary>
        /// ��Ԫ�ֲ�����X��
        /// </summary>
        LX,
        /// <summary>
        /// ��Ԫ�ֲ�����Y��
        /// </summary>
        LY,
        /// <summary>
        /// ��Ԫ�ֲ�����Z��
        /// </summary>
        LZ
    }
    /// <summary>
    /// ���ع�����
    /// </summary>
    [Serializable]
    public class BLoadCase
    {
        /// <summary>
        /// ���ع�������
        /// </summary>
        public string LCName;
        /// <summary>
        /// ���ع�������
        /// </summary>
        public LCType LCType;
    }

    /// <summary>
    /// ���ع������ϵ����
    /// </summary>
    [Serializable]
    public class BLCFactGroup
    {
        /// <summary>
        /// ��λ��������������
        /// </summary>
        public ANAL ANAL;
        /// <summary>
        /// ��������
        /// </summary>
        public string  LCNAME;
        /// <summary>
        /// ��λ���������ĺ���ϵ��
        /// </summary>
        public double FACT;
    }
    /// <summary>
    /// ���������
    /// </summary>
    [Serializable]
    public class BLoadComb
    {
        protected string _NAME;//�����������������
        protected LCKind _KIND;//������ϵ�����
        protected bool _bACTIVE;//�Ƿ񼤻�
        protected bool _bES;//������Ĳ�����һ���ΪNO
        protected int _iTYPE;//ָ��������Ϸ�ʽ��0Ϊ���ԣ�1Ϊ+SRSS,2Ϊ-SRSS��3Ϊƽ��������
        protected string _DESC;//��˵��
        protected List<BLCFactGroup> _LoadCombData;//�����������,һ��Ϊmgt�ļ��ڶ��к�����

        /// <summary>
        /// �����������������
        /// </summary>
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        /// <summary>
        /// ������ϵ�����
        /// </summary>
        public LCKind KIND
        {
            get { return _KIND; }
        }
        /// <summary>
        /// �����������
        /// </summary>
        public string DESC
        {
            get { return _DESC; }
            set {_DESC=value;}
        }
        /// <summary>
        /// ��ǰ����Ƿ񼤻�
        /// </summary>
        public bool bACTIVE
        {
            get { return _bACTIVE; }
            set { _bACTIVE = value; }
        }
        /// <summary>
        /// ָ��������Ϸ�ʽ��0Ϊ���ԣ�1Ϊ+SRSS,2Ϊ-SRSS��3Ϊƽ��������
        /// </summary>
        public int iTYPE
        {
            get { return _iTYPE; }
        }

        /// <summary>
        /// ȡ�ù����������
        /// </summary>
        public int Num_LCGroup
        {
            get { return _LoadCombData.Count; }
        }

        /// <summary>
        /// ���ع���ϵ��������
        /// </summary>
        public List<BLCFactGroup> LoadCombData
        {
            get { return _LoadCombData; }
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        public BLoadComb()
        {
            _LoadCombData = new List<BLCFactGroup>();
        }

        /// <summary>
        /// ������ϻ�����Ϣ
        /// </summary>
        /// <param name="Name">�����������</param>
        /// <param name="Kind">�����������</param>
        /// <param name="bActive">�Ƿ񼤻�</param>
        /// <param name="bEs">������Ĳ�����һ���ΪNO</param>
        /// <param name="iType">ָ��������Ϸ�ʽ��0Ϊ���ԣ�1Ϊ+SRSS,2Ϊ-SRSS</param>
        /// <param name="Desc">��˵��</param>
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
        /// ��Ӻ��ع������ϵ�����뵱ǰ���
        /// </summary>
        /// <param name="lcfg">���ع������ϵ����</param>
        public void AddLCFactGroup(BLCFactGroup lcfg)
        {
            _LoadCombData.Add(lcfg);
        }

        /// <summary>
        /// ������ϳ�ʼ�����Ƴ������������
        /// </summary>
        public void Clear()
        {
            _NAME="";
            _KIND=LCKind.GEN;
            _bACTIVE=true;
            _bES=false;
            _iTYPE=0;
            _DESC="";
            _LoadCombData.Clear();//�Ƴ�����Ԫ��
        }

        /// <summary>
        /// �жϵ�ǰ����Ƿ���ĳ������
        /// </summary>
        /// <param name="LCName">������</param>
        /// <returns>����Ϊtrue</returns>
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
    }

    /// <summary>
    /// ������ϱ�
    /// </summary>
    [Serializable]
    public class BLoadCombTable
    {
        #region ���ݳ�Ա
        private List<string> _ComGen;//һ����ϱ�
        private List<string> _ComSteel;//�ֽṹ�����ñ�
        private List<string> _ComCon;//�����������ñ�
        private Hashtable _LoadCombData;//����ģ����������ݹ�ϣ��
        #endregion
        #region ����
        /// <summary>
        /// һ����ϱ�
        /// </summary>
        public List<string> ComGen
        {
            get { return _ComGen; }
        }
        /// <summary>
        /// �ֽṹ�����ñ�
        /// </summary>
        public List<string> ComSteel
        {
            get { return _ComSteel; }
        }
        /// <summary>
        /// �����������ñ�
        /// </summary>
        public List<string> ComCon
        {
            get { return _ComCon; }
        }
        /// <summary>
        /// ģ��������������ݹ�ϣ��
        /// </summary>
        public Hashtable LoadCombData
        {
            get { return _LoadCombData; }
        }
        #endregion
        #region ���캯��
        public BLoadCombTable()
        {
            _ComGen = new List<string>();
            _ComSteel = new List<string>();
            _ComCon = new List<string>();
            _LoadCombData = new Hashtable();
        }
        #endregion
        #region ����
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="key">�ؼ���</param>
        /// <returns>�������</returns>
        public BLoadComb this[string key]
        {
            get
            {
                return _LoadCombData[key] as BLoadComb;
            }
        }
        /// <summary>
        /// ��Ӻ�������������
        /// </summary>
        /// <param name="com"></param>
        public void  Add(BLoadComb com)
        {
            //��¼ԭʼ���˳��
            switch (com.KIND)
            {
                case LCKind.GEN: _ComGen.Add(com.NAME); break;
                case LCKind.STEEL: _ComSteel.Add(com.NAME); break;
                case LCKind.CONC: _ComCon.Add(com.NAME); break;
                default: _ComGen.Add(com.NAME); break;
            }
            _LoadCombData.Add(com.NAME, com);//�������
        }
        /// <summary>
        /// �Ƴ�ָ���������
        /// </summary>
        /// <param name="comName"></param>
        public void Remove(string comName)
        {
            //��������������
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
                _LoadCombData.Remove(comName);//�Ƴ�����
            }
        }
        /// <summary>
        /// ������ϱ��Ƿ���ĳ�����
        /// </summary>
        /// <param name="Key">��Ϲؼ���</param>
        /// <returns>�ǻ��</returns>
        public bool ContainsKey(string Key)
        {
            return _LoadCombData.ContainsKey(Key);
        }
        #endregion
    }
    /// <summary>
    /// ������
    /// </summary>
    [Serializable]
    public abstract class Load
    {
        protected string group;//����
        protected string lc;//����
        /// <summary>
        /// ��������
        /// </summary>
        public abstract string Group
        {
            get;
            set;
        }
        /// <summary>
        /// ���ع���
        /// </summary>
        public abstract string LC
        {
            get;
            set;
        }

    }

    /// <summary>
    /// �ڵ������
    /// </summary>
    [Serializable]
    public class BNLoad : Load
    {
        private int node;//�ڵ��
        private double fx, fy, fz, mx, my, mz;
        /// <summary>
        /// �ڵ��
        /// </summary>
        public int iNode
        {
            get { return node; }
        }
        /// <summary>
        /// ��x�������
        /// </summary>
        public double FX
        {
            get { return fx; }
            set { fx = value; }
        }
        /// <summary>
        /// ��y�������
        /// </summary>
        public double FY
        {
            get { return fy; }
            set { fy = value; }
        }
        /// <summary>
        /// ��z�������
        /// </summary>
        public double FZ
        {
            get { return fz; }
            set { fz = value; }
        }
        /// <summary>
        /// x�����
        /// </summary>
        public double MX
        {
            get { return mx; }
            set { mx = value; }
        }
        /// <summary>
        /// y�����
        /// </summary>
        public double MY
        {
            get { return my; }
            set { my = value; }
        }
        /// <summary>
        /// z�����
        /// </summary>
        public double MZ
        {
            get { return mz; }
            set { mz = value; }
        }

        /// <summary>
        /// ���س��󷽷���Group
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
        /// ���캯������ʼ����ֵȫΪ0
        /// </summary>
        /// <param name="n">�ڵ��</param>
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
        /// �����޸Ľڵ��������
        /// </summary>
        /// <param name="data">�ڵ��������飺daouble[]{FX,FY,FZ,
        /// MX,MY,MZ}</param>
        public void putdata(double[] data)
        {
            //to do:....
        }
    }

    /// <summary>
    /// ���غ�����
    /// </summary>
    [Serializable]
    public class BWeight : Load
    {
        private double gx, gy, gz;
        /// <summary>
        /// �������ٶȳ���g=9.805
        /// </summary>
        private const double g = 9.805;//�������ٶ�ֵ
        /// <summary>
        /// �������ٶ�x��������
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
        /// �������ٶ�y��������
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
        /// �������ٶ�z��������
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
        /// �������ٶ�x����ֵ
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
        /// �������ٶ�y����ֵ
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
        /// �������ٶ�z����ֵ
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
        /// ���س��󷽷���Group
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
        /// ���س��󷽷���LC
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
    /// ����Ԫ������
    /// </summary>
    [Serializable]
    public class BBLoad : Load
    {
        private int elem_num;//��Ԫ��
        private string cmd, type;
        private DIR dir;//���ط���
        private bool bproj;//�Ƿ�ͶӰ

        private bool beccen;//�Ƿ�ƫ��
        private DIR eccdir;//ƫ�ķ���
        private double i_end, j_end;//i�˺�j�˵�ƫ�ĺ���ֵ
        private bool bj_end;//�Ƿ����j��ƫ�ĺ���

        private double d1, p1, d2, p2, d3, p3, d4, p4;//����������

        /// <summary>
        /// ��Ԫ���
        /// </summary>
        public int ELEM_num
        {
            get { return elem_num; }
            set { elem_num = value; }
        }

        /// <summary>
        /// BEAM��������Ԫ���أ�TYPITAL�����׼����Ԫ����
        /// </summary>
        public string CMD
        {
            get { return cmd; }
            set { cmd = value; }
        }

        /// <summary>
        /// ����Ԫ��������
        /// </summary>
        public string TYPE
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// ���ط���
        /// </summary>
        public DIR Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        /// <summary>
        /// �����Ƿ�ͶӰ
        /// </summary>
        public bool bPROJ
        {
            get { return bproj; }
            set { bproj = value; }
        }

        /// <summary>
        /// �����Ƿ�ƫ��
        /// </summary>
        public bool bECCEN
        {
            get { return beccen; }
            set { beccen = value; }
        }

        /// <summary>
        /// ƫ�ĺ��ط���
        /// </summary>
        public DIR EccDir
        {
            get { return eccdir; }
            set { eccdir = value; }
        }

        /// <summary>
        /// ��������
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
        /// ���ع�����
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
        /// ���캯��
        /// </summary>
        public BBLoad()
        {
            elem_num = 0;
            beccen = false;//��ƫ��
            bproj = false;//��ͶӰ
            cmd = "BEAM";
            type = "UNILOAD";
            beccen = false;
            dir = DIR.GX;
            bj_end = false;
        }

        /// <summary>
        /// ��ȡƫ�ĵ�Ԫ������Ϣ
        /// </summary>
        /// <param name="dataline">mgt�ļ�����Ԫ����������</param>
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
                setEccenDir(Ecc_Dir);//����ƫ�ķ���
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
        /// ����ƫ�ĺ�������
        /// </summary>
        /// <param name="bEccen">�Ƿ�ƫ��:"YES"/"NO"</param>
        /// <param name="Ecc_Dir">ƫ�ķ���</param>
        /// <param name="iData">i��ƫ�ľ�</param>
        /// <param name="jData">j��ƫ�ľ�</param>
        /// <param name="bJ_End">i�˺�j��ƫ���Ƿ���ͬ:"YES"/"NO"</param>
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
                    setEccenDir(Ecc_Dir);//����ƫ�ķ���
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
        /// ��������Ԫ������Ϣ����
        /// </summary>
        /// <param name="dd1">λ��1</param>
        /// <param name="pp1">����1</param>
        /// <param name="dd2">λ��2</param>
        /// <param name="pp2">����2</param>
        /// <param name="dd3">λ��3</param>
        /// <param name="pp3">����3</param>
        /// <param name="dd4">λ��4</param>
        /// <param name="pp4">����4</param>
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
        /// ���õ�Ԫ���ط���
        /// </summary>
        /// <param name="direction">���ط����ַ�����GX/GY/GZ/LX/LY/LZ</param>
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
        /// ����ƫ�ķ���
        /// </summary>
        /// <param name="direction">ƫ�ķ����ַ�����GX/GY/GZ/LX/LY/LZ</param>
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
        /// ��ȡ����Ԫ�������ݵ�λ��ֵ
        /// </summary>
        /// <param name="i">λ�ñ�ţ�1,2,3,4</param>
        /// <returns>λ��ֵ��0~1֮�����ֵ</returns>
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
        /// ��ȡ����Ԫ�������ݵĺ���ֵ
        /// </summary>
        /// <param name="i">����ֵ���</param>
        /// <returns>����ֵ</returns>
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

    #region Geometry Model Class(����ģ����)
    /// <summary>
    /// ����洢�ļ���Ϣ�Ľڵ��ࣺBnodes
    /// </summary>
    [Serializable]
    public class Bnodes : Object
    {
        /// <summary>
        /// �ڵ���
        /// </summary>
        public int num;
        /// <summary>
        /// �ڵ�X����
        /// </summary>
        public double X;
        /// <summary>
        /// �ڵ�Y����
        /// </summary>
        public double Y;
        /// <summary>
        /// �ڵ�Z����
        /// </summary>
        public double Z;
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="n">�ڵ���</param>
        public Bnodes(int n)
        {
            num = n;
            X = 0;
            Y = 0;
            Z = 0;
        }
        /// <summary>
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="n">�ڵ���</param>
        /// <param name="nx">�ڵ�X����</param>
        /// <param name="ny">�ڵ�Y����</param>
        /// <param name="nz">�ڵ�Z����</param>
        public Bnodes(int n, double nx, double ny, double nz)
        {
            num = n;
            X = nx;
            Y = ny;
            Z = nz;
        }

        //����ToString����
        new public string ToString()
        {
            return ("(" + num.ToString() + "," + X.ToString() + "," + Y.ToString() + "," + Z.ToString() + ")");
        }

        /// <summary>
        /// �����ڵ��ľ���
        /// </summary>
        /// <param name="nodeNext">��һ���ڵ�</param>
        /// <returns>���ؾ���ֵ</returns>
        public double DistanceTo(Bnodes nodeNext)
        {
            double res=Math.Sqrt(Math.Pow((nodeNext.X-this.X),2)+
                Math.Pow((nodeNext.Y-this.Y),2)+
                Math.Pow((nodeNext.Z-this.Z),2));
            return res;
        }
    }

    /// <summary>
    /// ��Ԫ����
    /// </summary>
    [Serializable]
    public abstract class Element : Object
    {
        private int _iEL, _iMAT, _iPRO;
        private ElemType _TYPE;
        /// <summary>
        /// ��Ԫ���
        /// </summary>
        public int iEL
        {
            get { return _iEL; }
            set { _iEL = value; }
        }
        /// <summary>
        /// ��Ԫ����
        /// </summary>
        public ElemType TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }
        /// <summary>
        /// ��Ԫ���Ϻ�
        /// </summary>
        public int iMAT
        {
            get { return _iMAT; }
            set { _iMAT = value; }
        }
        /// <summary>
        /// ��Ԫ����ֵ�ţ��������
        /// </summary>
        public int iPRO
        {
            get { return _iPRO; }
            set { _iPRO = value; }
        }
        /// <summary>
        /// ��Ԫ�ڵ������
        /// </summary>
        public List<int> iNs;

        /// <summary>
        ///���캯�� 
        /// </summary>
        public Element()
        {
            iEL = 0;
            TYPE = ElemType.NOTYPE;
            iMAT = 0;
            iPRO = 0;
            iNs = new List<int>();
        }
        /// <summary>
        /// ���캯������
        /// </summary>
        /// <param name="num">��Ԫ���</param>
        /// <param name="type">��Ԫ����</param>
        /// <param name="mat">��Ԫ���Ϻ�</param>
        /// <param name="pro">��Ԫ����ֵ�ţ�������</param>
        /// <param name="iNodes">�ڵ������</param>
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
        }
        //����

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
    /// ����Ԫ��
    /// </summary>
    [Serializable]
    public class FrameElement : Element
    {
        private double Angle;
        private int _iSUB;
        private double _EXVAL;
        private DesignParameters _DPs;

        /// <summary>
        /// �ֽṹ����Ԫ����Ʋ���
        /// </summary>
        public DesignParameters DPs
        {
            get { return _DPs; }
            set { _DPs = value; }
        }

        /// <summary>
        /// ����Ԫ����ǣ�beta�ǣ�
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
        /// �����ͣ�BEAM��TRUSS�޹أ�
        /// </summary>
        public int iSUB
        {
            set { _iSUB = value; }
            get { return _iSUB; }
        }
        /// <summary>
        /// ��Ԫ������������ݣ�BEAM��TRUSS�޹أ�
        /// </summary>
        public double EXVAL
        {
            set { _EXVAL = value; }
            get { return _EXVAL; }
        }
        /// <summary>
        /// ��������3
        /// </summary>
        public int iOPT;

        /// <summary>
        /// ���������Ĺ��캯��,beta=0;type="BEAM"
        /// </summary>
        public FrameElement()
            : base()
        {
            this.TYPE = ElemType.BEAM;
            this.beta = 0;
            this._DPs = new DesignParameters();
        }
        /// <summary>
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="num">��Ԫ��</param>
        /// <param name="type">��Ԫ����</param>
        /// <param name="mat">���Ϻ�</param>
        /// <param name="pro">�������Ժ�</param>
        /// <param name="iNodes">�ڵ������</param>
        public FrameElement(int num, ElemType type, int mat, int pro, params int[] iNodes)
            : base(num, type, mat, pro, iNodes)
        {
            //���û���Ĺ��캯��
            this._DPs = new DesignParameters();
        }

        #region ��Ԫ����
        #endregion
    }

    /// <summary>
    /// ƽ�浥Ԫ��
    /// </summary>
    [Serializable]
    public class PlanarElement : Element
    {
        private int _iSUB;
        private int _iWID;

        /// <summary>
        /// ��Ƚ����1
        /// </summary>
        public int iSUB
        {
            get { return _iSUB; }
            set { _iSUB = value; }
        }

        /// <summary>
        /// ��Ƚ����2
        /// </summary>
        public int iWID
        {
            get { return _iWID; }
            set { _iWID = value; }
        }

        /// <summary>
        /// ���������Ĺ��캯��
        /// </summary>
        public PlanarElement()
            : base()
        {
            this.TYPE = ElemType.PLATE;
        }

        /// <summary>
        /// ���û���Ĺ��캯��
        /// </summary>
        /// <param name="num">��Ԫ��</param>
        /// <param name="type">��Ԫ����</param>
        /// <param name="mat">���Ϻ�</param>
        /// <param name="pro">����ţ���Ⱥ�</param>
        /// <param name="iNodes">�ڵ������</param>
        public PlanarElement(int num, ElemType type, int mat, int pro, params int[] iNodes)
            : base(num, type, mat, pro, iNodes)
        {
            //���û���Ķ�Ӧ���캯��
        }
    }

    /// <summary>
    /// �洢�������ԵĻ���
    /// </summary>
    [Serializable]
    public abstract class BSections
    {
        /// <summary>
        /// �����
        /// </summary>
        private int iSEC;
        /// <summary>
        /// �������ͣ�ö��
        /// </summary>
       // public string TYPE;
        public SecType TYPE;
        /// <summary>
        /// ��������
        /// </summary>
        private string SNAME;
        /// <summary>
        /// ����ƫ������
        /// </summary>
        public ArrayList OFFSET;
        /// <summary>
        /// �Ƿ��Ǽ��б���
        /// </summary>
        public bool bsd;
        /// <summary>
        /// ������״��B��ʾ����
        /// </summary>
        public SecShape SSHAPE;
        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public ArrayList SEC_Data;


        #region �洢��������ֵ
        protected double _Area;//���
        protected double _ASy;//��Ԫ����ϵy�᷽�����Ч�������
        protected double _ASz;//��Ԫ����ϵz�᷽�����Ч�������
        private double _Ixx;//����Ťת���Ծ�
        /// <summary>
        /// ����Ťת���Ծ�
        /// </summary>
        public double Ixx
        {
            get { return _Ixx; }
            set { _Ixx = value; }
        }
        private double _Iyy;//��Ԫ��y��Ľ�����Ծ�
        /// <summary>
        /// ��Ԫ��y��Ľ�����Ծ�
        /// </summary>
        public  double Iyy
        {
            get { return _Iyy; }
            set { _Iyy = value; }
        }
        private double _Izz;//��Ԫ��z��Ľ�����Ծ�
        /// <summary>
        /// ��Ԫ��z��Ľ�����Ծ�
        /// </summary>
        public double Izz
        {
            get { return _Izz; }
            set { _Izz = value; }
        }

        private double _CyP;//���к��ᵽ��Ԫ����ϵ(+)y��������˵ľ���
        /// <summary>
        /// ���к��ᵽ��Ԫ����ϵ(+)y��������˵ľ���
        /// </summary>
        public double CyP
        {
            get { return _CyP; }
            set { _CyP = value; }
        }
        private double _CyM;//���к��ᵽ��Ԫ����ϵ(-)y��������˵ľ���
        /// <summary>
        /// ���к��ᵽ��Ԫ����ϵ(-)y��������˵ľ���
        /// </summary>
        public double CyM
        {
            get { return _CyM; }
            set { _CyM = value; }
        }
        private double _CzP;//���к��ᵽ��Ԫ����ϵ(+)z��������˵ľ���
        /// <summary>
        /// ���к��ᵽ��Ԫ����ϵ(+)z��������˵ľ���
        /// </summary>
        public double CzP
        {
            get { return _CzP; }
            set { _CzP = value; }
        }
        private double _CzM;//���к��ᵽ��Ԫ����ϵ(-)z��������˵ľ���
        /// <summary>
        /// ���к��ᵽ��Ԫ����ϵ(-)z��������˵ľ���
        /// </summary>
        public double CzM
        {
            get { return _CzM; }
            set { _CzM = value; }
        }
        protected double _QyB;//�����ڵ�Ԫ����ϵy�᷽��ļ���ϵ��
        protected double _QzB;//�����ڵ�Ԫ����ϵz�᷽��ļ���ϵ��
        protected double _PERI_OUT;//�����������ܳ�
        protected double _PERI_IN;//�����������ܳ�
        protected double _Cy;//��������y����
        protected double _Cz;//��������z����

        private double _y1;//�ĸ��ǵ�����
        //�ĸ��ǵ�����
        protected double Y1
        {
            get { return _y1; }
            set { _y1 = value; }
        }
        private double _z1;//�ĸ��ǵ�����
        //�ĸ��ǵ�����
        protected double Z1
        {
            get { return _z1; }
            set { _z1 = value; }
        }
        private double _y2;//�ĸ��ǵ�����
        //�ĸ��ǵ�����
        protected double Y2
        {
            get { return _y2; }
            set { _y2 = value; }
        }
        private double _z2;//�ĸ��ǵ�����
        //�ĸ��ǵ�����
        protected double Z2
        {
            get { return _z2; }
            set { _z2 = value; }
        }
        private double _y3;//�ĸ��ǵ�����
        //�ĸ��ǵ�����
        protected double Y3
        {
            get { return _y3; }
            set { _y3 = value; }
        }
        private double _z3;//�ĸ��ǵ�����
        //�ĸ��ǵ�����
        protected double Z3
        {
            get { return _z3; }
            set { _z3 = value; }
        }
        private double _y4;//�ĸ��ǵ�����
        //�ĸ��ǵ�����
        protected double Y4
        {
            get { return _y4; }
            set { _y4 = value; }
        }
        private double _z4;//�ĸ��ǵ�����
        //�ĸ��ǵ�����
        protected double Z4
        {
            get { return _z4; }
            set { _z4 = value; }
        }
        #endregion
        /// <summary>
        /// ������������
        /// </summary>
        public string Name
        {
            get { return SNAME; }
            set { SNAME = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public int Num
        {
            get { return iSEC; }
            set { iSEC = value; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public double Area
        {
            get { return _Area; }
        }

        /// <summary>
        /// ���캯��
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
        /// ��һ����ʽ�������������Ϣ
        /// </summary>
        /// <returns>ansys�������ַ���</returns>
        public abstract string WriteData();

        /// <summary>
        /// ���°����ݼ���������ԣ���������Ծص�
        /// </summary>
        public abstract void  CalculateSecProp();

        /// <summary>
        /// ���ý��泣������ֵ1
        /// </summary>
        /// <param name="area">���</param>
        /// <param name="asy">y����Ч�������</param>
        /// <param name="asz">z����Ч�������</param>
        /// <param name="ixx">Ťת���Ծ�</param>
        /// <param name="iyy">��y��Ĺ��Ծ�</param>
        /// <param name="izz">��z��Ĺ��Ծ�</param>
        public void setSecProp1(double area, double asy, double asz, double ixx, double iyy, double izz)
        {
            _Area = area; _ASy = asy; _ASz = asz;
            _Ixx = ixx; _Iyy = iyy; _Izz = izz;
        }

        /// <summary>
        /// ���ý��泣������ֵ2
        /// </summary>
        /// <param name="cyp">���к��ᵽ��Ԫ����ϵ(+)y��������˵ľ���</param>
        /// <param name="cym">���к��ᵽ��Ԫ����ϵ(-)y��������˵ľ���</param>
        /// <param name="czp">���к��ᵽ��Ԫ����ϵ(+)z��������˵ľ���</param>
        /// <param name="czm">���к��ᵽ��Ԫ����ϵ(-)z��������˵ľ���</param>
        /// <param name="qyb">�����ڵ�Ԫ����ϵy�᷽��ļ���ϵ��</param>
        /// <param name="qzb">�����ڵ�Ԫ����ϵz�᷽��ļ���ϵ��</param>
        /// <param name="p_out">�����������ܳ�</param>
        /// <param name="p_in">�����������ܳ�</param>
        /// <param name="cy">��������y����</param>
        /// <param name="cz">��������y����</param>
        public void setSecProp2(double cyp, double cym, double czp, double czm, double qyb, double qzb,
            double p_out, double p_in, double cy, double cz)
        {
            _CyP = cyp; _CyM = cym; _CzP = czp; _CzM = czm; _QyB = qyb; _QzB = qzb;
            _PERI_OUT = p_out; _PERI_IN = p_in;
        }

        /// <summary>
        /// ���ý��泣������ֵ3
        /// </summary>
        /// <param name="y1">���ϵ�y����</param>
        /// <param name="z1">���ϵ�z����</param>
        /// <param name="y2">���ϵ�y����</param>
        /// <param name="z2">���ϵ�z����</param>
        /// <param name="y3">���µ�y����</param>
        /// <param name="z3">���µ�z����</param>
        /// <param name="y4">���µ�y����</param>
        /// <param name="z4">���µ�z����</param>
        public void setSecProp3(double y1, double z1, double y2, double z2, double y3, double z3, double y4, double z4)
        {
            _y1 = y1; _z1 = z1; _y2 = y2; _z2 = z2;
            _y3 = y3; _z3 = z3; _y4 = y4; _z4 = z4;
        }
    }

    /// <summary>
    /// ���ý�����Ϣ��
    /// </summary>
    [Serializable]
    public class SectionDBuser : BSections
    {
        public SectionDBuser():base()
        {
            this.TYPE = SecType.DBUSER;
            //���½�������
            CalculateSecProp();
        }

        /// <summary>
        /// �������ansys ����������
        /// </summary>
        /// <returns>ansys������</returns>
        public override string WriteData()
        {
            //throw new NotImplementedException();
            string res = null;
            if (this.SSHAPE == SecShape.B && (int)this.SEC_Data[0] == 2)//���ν���
            {
                res += "sectype," + this.Num.ToString() + ",beam,hrec," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString() + "," + SEC_Data[3].ToString() + "," + SEC_Data[3].ToString()
                + "," + SEC_Data[6].ToString() + "," + SEC_Data[4].ToString();
            }
            else if (this.SSHAPE== SecShape.H && (int)this.SEC_Data[0] == 2)//H�ͽ���
            {
                res += "sectype," + this.Num.ToString() + ",beam,i," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString() + "," + SEC_Data[4].ToString()
                    + "," + SEC_Data[4].ToString() + "," + SEC_Data[3].ToString();
            }
            else if (this.SSHAPE == SecShape.P && (int)this.SEC_Data[0] == 2)//Բ�ܽ���
            {
                double ri = (double)SEC_Data[1] / 2 - (double)SEC_Data[2];
                double ro = (double)SEC_Data[1] / 2;
                res += "sectype," + this.Num.ToString() + ",beam,ctube," + this.Name;
                res += "\nsecdata," + ri.ToString() + "," + ro.ToString();
            }
            else if (this.SSHAPE==SecShape .SB&& (int)this.SEC_Data[0] == 2)//���ν���
            {
                res += "sectype," + this.Num.ToString() + ",beam,rect," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString();
            }
            else if (this.SSHAPE== SecShape .T&& (int)this.SEC_Data[0] == 2)//T�ͽ���
            {
                res += "sectype," + this.Num.ToString() + ",beam,t," + this.Name;
                res += "\nsecdata," + SEC_Data[1].ToString() + "," + SEC_Data[2].ToString() + "," + SEC_Data[3].ToString() + "," +
                    SEC_Data[4].ToString();
            }
            else if (this.SSHAPE==SecShape.SR && (int)this.SEC_Data[0] == 2)//Բ�ν���
            {
                res += "sectype," + this.Num.ToString() + ",beam,csolid," + this.Name;
                res += "\nsecdata," + ((double)SEC_Data[1] / 2).ToString();
            }
            else if (this.SSHAPE==SecShape.C&& (int)this.SEC_Data[0]==2)//�۸ֽ���
            {
                res += "sectype," + this.Num.ToString() + ",beam,chan," + this.Name;
                res += "\nsecdata," + SEC_Data[5].ToString() + "," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString() + "," +
                    SEC_Data[6].ToString() + "," + SEC_Data[4].ToString() + "," + SEC_Data[3].ToString();
            }
            else
            {
                res += "!�˽�����״��Ϣδ����" + this.Num.ToString();
            }
            return res;
        }

        /// <summary>
        /// ���ؼ����������
        /// </summary>
        public override void CalculateSecProp()
        {
            //throw new NotImplementedException();
            if (this.SSHAPE == SecShape.B && (int)this.SEC_Data[0] == 2)//���ν���
            {
                this._Area = 0;
                //todo:�������ʵ��
            }
            else if (this.SSHAPE == SecShape.H && (int)this.SEC_Data[0] == 2)//H�ͽ���
            {
                this._Area = 0;
                //todo:�������ʵ��
            }
            else if (this.SSHAPE == SecShape.P && (int)this.SEC_Data[0] == 2)//Բ�ܽ���
            {
                double ri = (double)SEC_Data[1] / 2 - (double)SEC_Data[2];
                double ro = (double)SEC_Data[1] / 2;
                this._Area = Math.PI * Math.Pow(ro, 2) - Math.PI * Math.Pow(ri, 2);
            }
            else if (this.SSHAPE == SecShape.SB && (int)this.SEC_Data[0] == 2)//���ν���
            {
                this._Area = 0;
                //todo:�������ʵ��
            }
            else if (this.SSHAPE == SecShape.T && (int)this.SEC_Data[0] == 2)//T�ͽ���
            {
                this._Area = 0;
                //todo:�������ʵ��
            }
            else if (this.SSHAPE == SecShape.SR && (int)this.SEC_Data[0] == 2)//Բ�ν���
            {
                double rr = (double)SEC_Data[1] / 2;
                this._Area = Math.PI * Math.Pow(rr, 2);
            }
            else if (this.SSHAPE == SecShape.C && (int)this.SEC_Data[0] == 2)//�۸ֽ���
            {
                this._Area = 0;
                //todo:�������ʵ��
            }
            else
            {
                this._Area=0;
            }
        }
    }

    /// <summary>
    /// ���������Ϣ��
    /// </summary>
    public class SectionTapered : BSections
    {
        /// <summary>
        /// ���ǵ�Ԫ����ϵy�������صķ���
        /// </summary>
        public iVAR iyVAR;
        /// <summary>
        /// ���ǵ�Ԫ����ϵz�������صķ���
        /// </summary>
        public iVAR izVAR;

        /// <summary>
        /// �ӽ�����������
        /// </summary>
        public STYPE SubTYPE;
        
        /// <summary>
        /// ���������Ĺ��캯��
        /// </summary>
        public SectionTapered()
            : base()
        {
            this.TYPE = SecType.TAPERED;
            iyVAR = iVAR.Linear;
            izVAR = iVAR.Linear;
            SubTYPE = STYPE.USER;

            //���½�������
            CalculateSecProp();
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <returns>ansys���涨������</returns>
        public override string WriteData()
        {
            //throw new NotImplementedException();
            string res = "!�˽���ΪTAPERED�������룬��Ϣδ����:" + this.Num.ToString();
            return res;
        }
        
        /// <summary>
        /// ���ؼ����������
        /// </summary>
        public override void CalculateSecProp()
        {
            //throw new NotImplementedException();
            this._Area = 0;
            //todo:�������ʵ��
        }
    }

    /// <summary>
    /// �Զ���SPC������Ϣ��
    /// </summary>
    [Serializable]
    public class SectionGeneral : BSections
    {
        private Point2dCollection _OPOLY;
        private List<Point2dCollection> _IPOLYs;
        private bool _bBU;
        private bool _bEQ;


        /// <summary>
        /// δ֪����
        /// </summary>
        public bool bBU
        {
            set { _bBU = value; }
            get { return _bBU; }
        }

        /// <summary>
        /// δ֪����
        /// </summary>
        public bool bEQ
        {
            set { _bEQ = value; }
            get { return _bEQ; }
        }
        /// <summary>
        /// ���캯��
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
        /// ���Ansys������Ϣ
        /// </summary>
        /// <returns>APDL����������� SectionGeneral</returns>
        public override string WriteData()
        {
            //throw new NotImplementedException();
            string res = null;
            res = "!�˽���ΪSPC�Զ�����棡����";
            return res;
        }
        /// <summary>
        /// ����������ԣ� SectionGeneral�����ʲôҲ����
        /// </summary>
        public override void CalculateSecProp()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// �������������ӿ��Ƶ�
        /// </summary>
        /// <param name="pt">���</param>
        public void addtoOPOLY(Point2d pt)
        {
            _OPOLY.addPt(pt);
        }
        /// <summary>
        /// �������������ӿ��Ƶ�
        /// </summary>
        /// <param name="index">��������������</param>
        /// <param name="pt">Ҫ���ӵĵ��</param>
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
    /// �洢�嵥Ԫ�����Ϣ����
    /// </summary>
    [Serializable]
    public class BThickness
    {
        private int _iTHK;
        private string _TYPE;
        private bool _bSAME;
        private double _THIK_IN, _THIK_OUT;

        /// <summary>
        /// ��Ƚ����
        /// </summary>
        public int iTHK
        {
            set { _iTHK = value; }
            get { return _iTHK; }
        }

        /// <summary>
        /// ��Ƚ�������
        /// </summary>
        public string TYPE
        {
            set { _TYPE = value; }
            get { return _TYPE; }
        }

        /// <summary>
        /// �Ƿ�ƽ������ͬһ����
        /// </summary>
        public bool bSAME
        {
            set { _bSAME = value; }
            get { return _bSAME; }
        }

        /// <summary>
        /// �嵥Ԫ���ں��
        /// </summary>
        public double THIK_IN
        {
            set { _THIK_IN = value; }
            get { return _THIK_IN; }
        }

        /// <summary>
        /// �嵥Ԫ������
        /// </summary>
        public double THIK_OUT
        {
            set { _THIK_OUT = value; }
            get { return _THIK_OUT; }
        }
    }

    /// <summary>
    /// �����������࣬ö��
    /// </summary>
    public enum SecType
    {
        /// <summary>
        /// ��DB������ģ������������͵Ľ���
        /// </summary>
        DBUSER,
        /// <summary>
        /// ֱ�����������������
        /// </summary>
        VALUE,
        /// <summary>
        /// SRC��������
        /// </summary>
        SRC,
        /// <summary>
        /// ��Ͻ���
        /// </summary>
        COMBINED,
        /// <summary>
        /// �������
        /// </summary>
        TAPERED
    }

    /// <summary>
    /// ������״��ö��
    /// </summary>
    public enum SecShape
    {
        /// <summary>
        /// Angle �Ǹ�
        /// </summary>
        L,
        /// <summary>
        /// Channel �۸� 
        /// </summary>
        C,
        /// <summary>
        /// H�͸�
        /// </summary>
        H,
        /// <summary>
        /// T�͸�
        /// </summary>
        T,
        /// <summary>
        /// Box ����
        /// </summary>
        B,
        /// <summary>
        /// Pipe �ֹ�
        /// </summary>
        P,
        /// <summary>
        /// Solid Rectangle ʵ����
        /// </summary>
        SB,
        /// <summary>
        /// Solid Round ʵԲ��
        /// </summary>
        SR,
        /// <summary>
        /// Cold Formed Channel ����۸�
        /// </summary>
        CC,
        /// <summary>
        /// �Զ������
        /// </summary>
        GEN
    }

    /// <summary>
    /// ���ǽ��������Ծصķ�����������TAPERED�������ͣ�
    /// </summary>
    public enum iVAR
    {
        /// <summary>
        /// ֱ����
        /// </summary>
        Linear=1,
        /// <summary>
        /// ��������
        /// </summary>
        Parabolic=2,
        /// <summary>
        /// ����������
        /// </summary>
        Cubic=3
    }

    /// <summary>
    /// �ӽ�����״�����������ͣ�������TAPERED�������ͣ�
    /// </summary>
    public enum STYPE
    {
        /// <summary>
        /// ������׼����
        /// </summary>
        DB,
        /// <summary>
        /// �û����붨�ͽ���ߴ�
        /// </summary>
        USER,
        /// <summary>
        /// ʹ��VALUE�������
        /// </summary>
        VALUE
    }

    /// <summary>
    /// ��Ԫ���ͣ�ö��
    /// </summary>
    public enum ElemType
    {
        /// <summary>
        /// ��ܵ�Ԫ
        /// </summary>
        TRUSS,
        /// <summary>
        /// ����Ԫ
        /// </summary>
        BEAM,
        /// <summary>
        /// ֻ������Ԫ
        /// </summary>
        TENSTR,
        /// <summary>
        /// ֻ��ѹ��Ԫ
        /// </summary>
        COMPTR,
        /// <summary>
        /// ƽ��嵥Ԫ
        /// </summary>
        PLATE,
        /// <summary>
        /// ƽ��Ӧ����Ԫ
        /// </summary>
        PLSTRS,
        /// <summary>
        /// ƽ��Ӧ�䵥Ԫ
        /// </summary>
        PLSTRN,
        /// <summary>
        /// ��ԳƵ�Ԫ
        /// </summary>
        AXISYM,
        /// <summary>
        /// ʵ�嵥Ԫ
        /// </summary>
        SOLID,
        /// <summary>
        /// δ֪��Ԫ
        /// </summary>
        NOTYPE
    }
    #endregion

    #region Constraint (�߽�Լ����)
    /// <summary>
    /// �߽�������
    /// </summary>
    [Serializable]
    public class BConstraint : Object
    {
        /// <summary>
        /// �ڵ���
        /// </summary>
        public ArrayList node_list;
        private bool cUX;
        private bool cUY;
        private bool cUZ;
        private bool cRX;
        private bool cRY;
        private bool cRZ;
        //�����ֶ�
        /// <summary>
        /// �Ƿ�Լ��UX
        /// </summary>
        public bool UX
        {
            get { return cUX; }
            set { cUX = value; }
        }
        /// <summary>
        /// �Ƿ�Լ��UY
        /// </summary>
        public bool UY
        {
            get { return cUY; }
            set { cUY = value; }
        }
        /// <summary>
        /// �Ƿ�Լ��UZ
        /// </summary>
        public bool UZ
        {
            get { return cUZ; }
            set { cUZ = value; }
        }
        /// <summary>
        /// �Ƿ�Լ��RX
        /// </summary>
        public bool RX
        {
            get { return cRX; }
            set { cRX = value; }
        }
        /// <summary>
        /// �Ƿ�Լ��RY
        /// </summary>
        public bool RY
        {
            get { return cRY; }
            set { cRY = value; }
        }
        /// <summary>
        /// �Ƿ�Լ��RZ
        /// </summary>
        public bool RZ
        {
            get { return cRZ; }
            set { cRZ = value; }
        }

        /// <summary>
        /// ���캯��
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
    /// ��������ֵ��
    /// </summary>
    [Serializable]
    public class BMaterial:Object 
    {
        private int _iMAT;//���ϱ��
        private MatType _TYPE;//��������
        private string _MNAME;//��������
        private double _Elast;//����ģ��
        private double _Poisn;//���ɱ�
        private double _Thermal;//������ϵ��
        private double _Den;//��λ�������
        private double _Fy;//��������ǿ��

        /// <summary>
        /// ԭʼmgt������Ϣ
        /// </summary>
        public ArrayList MGT_Data;

        /// <summary>
        /// ���Ϻ�
        /// </summary>
        public int iMAT
        {
            get { return _iMAT; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public MatType TYPE
        {
            get { return _TYPE; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string MNAME
        {
            get { return _MNAME; }
        }

        /// <summary>
        /// ����ģ��
        /// </summary>
        public double Elast
        {
            get { return _Elast; }
        }
        /// <summary>
        /// ���ɱ�
        /// </summary>
        public double Poisn
        {
            get { return _Poisn; }
        }
        /// <summary>
        /// ������ϵ��
        /// </summary>
        public double Thermal
        {
            get { return _Thermal; }
        }
        /// <summary>
        /// ��λ�������
        /// </summary>
        public double Den
        {
            get { return _Den; }
        }

        /// <summary>
        /// ��������ǿ��
        /// </summary>
        public double Fy
        {
            get { return _Fy; }
        }
        /// <summary>
        /// ���캯��:Ĭ���Ǹֵ�����
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
        /// ���캯��
        /// </summary>
        /// <param name="num">���Ϻ�</param>
        /// <param name="type">��������</param>
        /// <param name="name">��������</param>
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
        /// ���캯��
        /// </summary>
        /// <param name="num">���Ϻ�</param>
        /// <param name="type">��������</param>
        /// <param name="name">��������</param>
        /// <param name="E">��ģ��Pa��</param>
        /// <param name="Poi">���ɱ�</param>
        /// <param name="Ther">������ϵ��</param>
        /// <param name="den">�����ܶȣ�kg/m3��</param>
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

        //����
        /// <summary>
        /// ���õ�ģ�Ȼ�������
        /// </summary>
        /// <param name="E">��ģ��Pa��</param>
        /// <param name="Poi">���ɱ�</param>
        /// <param name="Ther">������ϵ��</param>
        /// <param name="den">�����ܶȣ�kg/m3��</param>
        public void setProp(double E, double Poi, double Ther, double den)
        {
            _Elast = E;
            _Poisn = Poi;
            _Thermal = Ther;
            _Den = den;
        }

        /// <summary>
        /// �洢MGT�ļ�ԭʼ��¼��Ϣ
        /// </summary>
        /// <param name="data">mgt�ļ�������</param>
        public void addMGTdata(string data)
        {
            string[] temp = data.Split(',');

            foreach (string dt in temp)
            {
                MGT_Data.Add(dt.Trim());
            }
        }
        /// <summary>
        /// ��׼����������ֵ
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
    /// �������ͣ�ö��
    /// </summary>
    public enum MatType
    {
        /// <summary>
        /// ��
        /// </summary>
        STEEL,
        /// <summary>
        /// ������
        /// </summary>
        CONC,
        /// <summary>
        /// ���������
        /// </summary>
        SRC,
        /// <summary>
        /// �û��Զ���
        /// </summary>
        USER
    }
    #endregion

    #region ��Ԫ������
    /// <summary>
    /// ��Ԫ����������
    /// </summary>
    [Serializable]
    public class SecForce
    {
        private double _N, _T, _Vy, _Vz, _My, _Mz;
        /// <summary>
        /// ������(��Ϊ����ѹΪ��)
        /// </summary>
        public double N
        {
            get { return _N; }
        }
        /// <summary>
        /// Ť��
        /// </summary>
        public double T
        {
            get { return _T; }
        }
        /// <summary>
        /// �ص�Ԫy��ļ���
        /// </summary>
        public double Vy
        {
            get { return _Vy; }
        }
        /// <summary>
        /// �ص�Ԫz��ļ���
        /// </summary>
        public double Vz
        {
            get { return _Vz; }
        }
        /// <summary>
        /// �Ƶ�Ԫy������
        /// </summary>
        public double My
        {
            get { return _My; }
        }
        /// <summary>
        /// �Ƶ�Ԫz������
        /// </summary>
        public double Mz
        {
            get { return _Mz; }
        }

        /// <summary>
        /// ���캯��1
        /// </summary>
        public SecForce()
        {
            this.SetAllForces(0, 0, 0, 0, 0, 0);
        }
        /// <summary>
        /// ���캯��2
        /// </summary>
        /// <param name="N">����/kN/m</param>
        /// <param name="T">Ť��/kN/m</param>
        /// <param name="Vy">����/kN/m</param>
        /// <param name="Vz">����/kN/m</param>
        /// <param name="My">���/kN/m</param>
        /// <param name="Mz">���/kN/m</param>
        public SecForce(double N, double T, double Vy, double Vz, double My, double Mz)
        {
            this.SetAllForces(N, T, Vy, Vz, My, Mz);
        }
        /// <summary>
        /// ָ����������
        /// </summary>
        /// <param name="N">����/kN/m</param>
        /// <param name="T">Ť��/kN/m</param>
        /// <param name="Vy">����/kN/m</param>
        /// <param name="Vz">����/kN/m</param>
        /// <param name="My">���/kN/m</param>
        /// <param name="Mz">���/kN/m</param>
        public void SetAllForces(double N,double T,double Vy,double Vz,double My,double Mz)
        {
            _N = N; _T = T;
            _Vy = Vy; _Vz = Vz;
            _My = My; _Mz = Mz;
        }

        /// <summary>
        /// ��������������ط���
        /// </summary>
        /// <param name="sf1">��������1</param>
        /// <param name="sf2">��������2</param>
        /// <returns>��Ӻ�Ľ�������</returns>
        public static SecForce operator +(SecForce sf1, SecForce sf2)
        {
            SecForce res = new SecForce();
            res.SetAllForces(sf1.N + sf2.N, sf1.T + sf2.T, sf1.Vy + sf2.Vy,
                sf1.Vz + sf2.Vz, sf1.My + sf2.My, sf1.Mz + sf2.Mz);
            return res;
        }

        /// <summary>
        /// ���������Գ�ϵ��
        /// </summary>
        /// <param name="fact">����</param>
        /// <returns>��������</returns>
        public  SecForce Mutiplyby(double fact)
        {
            SecForce res = new SecForce(N * fact, T * fact, Vy * fact,
                Vz * fact, My * fact, Mz * fact);
            return res;
        }
    }
    /// <summary>
    /// �洢��Ԫ��������
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
        #region ������
        /// <summary>
        /// ��Ԫi����������
        /// </summary>
        public SecForce Force_i
        {
            get { return _Force_i; }
        }
        /// <summary>
        /// ��Ԫ1/8����������
        /// </summary>
        public SecForce Forcce_18
        {
            get { return _Force_18; }
        }
        /// <summary>
        /// ��Ԫ2/8����������
        /// </summary>
        public SecForce Force_28
        {
            get { return _Force_28; }
        }
        /// <summary>
        /// ��Ԫ3/8����������
        /// </summary>
        public SecForce Force_38
        {
            get { return _Force_38; }
        }
        /// <summary>
        /// ��Ԫ�е���洦������
        /// </summary>
        public SecForce Force_48
        {
            get { return _Force_48; }
        }
        /// <summary>
        /// ��Ԫ5/8����������
        /// </summary>
        public SecForce Force_58
        {
            get { return _Force_58; }
        }
        /// <summary>
        /// ��Ԫ6/8����������
        /// </summary>
        public SecForce Force_68
        {
            get { return _Force_68; }
        }
        /// <summary>
        /// ��Ԫ7/8���Ľ�������
        /// </summary>
        public SecForce Force_78
        {
            get { return _Force_78; }
        }
        /// <summary>
        /// ��Ԫj�˽�������
        /// </summary>
        public SecForce Force_j
        {
            get { return _Force_j; }
        }
        #endregion
        #region �෽��

        /// <summary>
        /// ���캯��
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
        /// ���뵥Ԫ����
        /// </summary>
        /// <param name="Fi">��Ԫi�˽�������</param>
        /// <param name="Fj">��Ԫj�˽�������</param>
        public void SetElemForce(SecForce Fi, SecForce Fj)
        {
            _Force_i = Fi;
            _Force_j = Fj;
        }
        /// <summary>
        /// ���뵥Ԫ�������������棩
        /// </summary>
        /// <param name="Fi">��Ԫi�˽�������</param>
        /// <param name="F48">��Ԫ�н�������</param>
        /// <param name="Fj">��Ԫj�˽�������</param>
        public void SetElemForce(SecForce Fi, SecForce F48, SecForce Fj)
        {
            _Force_i = Fi; _Force_j = Fj;
            _Force_48 = F48;
        }
        /// <summary>
        /// ���뵥Ԫ������ÿ��һ������
        /// </summary>
        /// <param name="F">Ҫ����Ľ�������</param>
        /// <param name="num">����ţ�0����i�˽��棬8����j�˽���</param>
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
        /// ������
        /// </summary>
        /// <param name="index">������</param>
        /// <returns>���ؽ�������</returns>
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
        /// ���ص�Ԫ������������
        /// </summary>
        /// <param name="ef1">��Ԫ����1</param>
        /// <param name="ef2">��Ԫ����2</param>
        /// <returns>��Ӻ�ĵ�Ԫ����</returns>
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
        /// ��Ԫ�����Գ�ϵ��
        /// </summary>
        /// <param name="fact">ϵ��</param>
        /// <returns>��Ԫ����</returns>
        public ElemForce Mutiplyby(double fact)
        {
            ElemForce res = new ElemForce();
            for (int i = 0; i < 9; i++)
            {
                res[i]=this[i].Mutiplyby(fact);
            }
            return res;
        }
        #endregion
    }

    /// <summary>
    /// ��Ԫ������
    /// </summary>
    [Serializable]
    public class BElemForceTable:Object
    {
        private int _elem;
        private SortedList<string,ElemForce> _LCForces;

        /// <summary>
        /// ��Ԫ��
        /// </summary>
        public int elem
        {
            get { return _elem; }
            set { _elem = value; }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public SortedList<string, ElemForce> LCForces
        {
            get { return _LCForces; }
            set { _LCForces = value; }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public BElemForceTable()
        {
            _elem = 0;
            _LCForces = new SortedList<string, ElemForce>();
        }

        /// <summary>
        /// ����Ԫ����ӹ�������
        /// </summary>
        /// <param name="lc">��������</param>
        /// <param name="force">��������</param>
        public void add_LCForce(string lc, ElemForce force)
        {
            _LCForces.Add(lc, force);
        }
        /// <summary>
        /// �ж��Ƿ�����Ӧ�����
        /// </summary>
        /// <param name="lc"></param>
        /// <returns></returns>
        public bool hasLC(string lc)
        {
            return _LCForces.ContainsKey(lc);
        }
    }
    #endregion

    /// <summary>
    /// ģ���ࣺ��װ����������Ϣ
    /// </summary>
    [Serializable]
    public class Bmodel : Object
    {
        #region ��Ա
        /// <summary>
        /// ��λϵͳ
        /// </summary>
        public BUNIT unit;
        /// <summary>
        /// �ڵ���Ϣ�б�
        /// </summary>
        public SortedList<int, Bnodes> nodes;
        /// <summary>
        /// ��Ԫ��Ϣ�б�
        /// </summary>
        public SortedList<int, Element> elements;
        /// <summary>
        /// ������Ϣ�б�
        /// </summary>
        public SortedList<int, BSections> sections;
        /// <summary>
        /// �嵥Ԫ��ȱ�
        /// </summary>
        public SortedList<int, BThickness> thickness;

        /// <summary>
        /// Լ����Ϣ
        /// </summary>
        public List<BConstraint> constraint;

        /// <summary>
        /// ���ع����б�
        /// </summary>
        public List<BLoadCase> STLDCASE;

        /// <summary>
        /// ��������б�
        /// </summary>
        //public SortedList<string, BLoadComb> LOADCOMBS;
         private BLoadCombTable _LoadCombTable;

        /// <summary>
        /// �ڵ��������
        /// </summary>
        public SortedList<int, BNLoad> conloads;

        /// <summary>
        /// ����Ԫ��������
        /// </summary>
        public SortedList<int, BBLoad> beamloads;

        /// <summary>
        /// ���غ�����Ϣ����
        /// </summary>
        public SortedList<string, BWeight> selfweight;

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public SortedList<int, BMaterial> mats;

        /// <summary>
        /// ��Ԫ��������
        /// </summary>
        public SortedList<int, BElemForceTable> elemforce;
        #endregion

        #region ����
        /// <summary>
        /// ��������ֵ�
        /// </summary>
        public BLoadCombTable LoadCombTable
        {
            get { return _LoadCombTable; }
        }
        #endregion
        /// <summary>
        /// ���캯��
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
            _LoadCombTable = new BLoadCombTable();//������ϱ�
            conloads = new SortedList<int, BNLoad>(new RepeatedKeySort());//�ڵ����
            beamloads = new SortedList<int, BBLoad>(new RepeatedKeySort());//����Ԫ����
            selfweight = new SortedList<string, BWeight>();//������Ϣ
            mats = new SortedList<int, BMaterial>();//������Ϣ

            elemforce = new SortedList<int, BElemForceTable>();//��Ԫ������
        }
        #region ģ�ʹ�����
        /// <summary>
        /// ת������Ԫ�ؼ�����Ϣ
        /// </summary>
        public void GenBeamKpoint()
        {
            int Nnodes = 99999;//ģ�ͽڵ������������ڷ�������ʼ���
            int i = 1;
            foreach (Element elee in this.elements.Values)
            {
                FrameElement ele = elee as FrameElement;
                if (ele != null && ele.beta != 0 && ele.iNs.Count < 3)
                {
                    Bnodes ndi = this.nodes[(int)ele.iNs[0]];//�õ��ڵ�i
                    Bnodes ndj = this.nodes[(int)ele.iNs[1]];//�õ��ڵ�j

                    if (ndi.X == ndj.X && ndi.Y == ndj.Y)
                    {
                        //�����Ԫ����ϵx��ƽ����ȫ������ϵz��
                        Point3d pto = new Point3d(ndi.X + 10000, ndi.Y, ndi.Z);
                        Point3d pt1 = new Point3d(ndi.X, ndi.Y, ndi.Z);
                        Point3d pt2 = new Point3d(ndj.X, ndj.Y, ndj.Z);

                        Point3d ptk = RotNodebyAxis(pto, pt1, pt2, -ele.beta * Math.PI / 180);//��÷����

                        Bnodes ndk = new Bnodes(Nnodes + i, ptk.X, ptk.Y, ptk.Z);
                        nodes.Add(Nnodes + i, ndk);//��ӷ���ڵ��ģ�����ݿ�

                        ele.iNs.Add(Nnodes + i);// ��ӷ���ڵ�ŵ���Ԫ����
                    }
                    else
                    {
                        //�����Ԫ����ϵx�᲻ƽ����ȫ������ϵz��
                        Point3d pto = new Point3d(ndi.X, ndi.Y, ndi.Z + 10000);
                        Point3d pt1 = new Point3d(ndi.X, ndi.Y, ndi.Z);
                        Point3d pt2 = new Point3d(ndj.X, ndj.Y, ndj.Z);

                        Point3d ptk = RotNodebyAxis(pto, pt1, pt2, -ele.beta * Math.PI / 180);//��÷����

                        Bnodes ndk = new Bnodes(Nnodes + i, ptk.X, ptk.Y, ptk.Z);
                        nodes.Add(Nnodes + i, ndk);//��ӷ���ڵ��ģ�����ݿ�

                        ele.iNs.Add(Nnodes + i);// ��ӷ���ڵ�ŵ���Ԫ����
                    }

                    i++;
                }
            }
        }
        /// <summary>
        /// ����ռ������������ת����ǶȺ�Ľ��
        /// </summary>
        /// <param name="pt_original">ԭʼ�ڵ�</param>
        /// <param name="pt1_Axis">ת�����</param>
        /// <param name="pt2_Axis">ת���յ�</param>
        /// <param name="A">ת��,�Ի��ȼ���</param>
        /// <returns>��ת��Ľڵ�###���Ϊת�᷽������۲��ߣ�˳ʱ��תȥ�õ�</returns>
        public static Point3d RotNodebyAxis(Point3d pt_original, Point3d pt1_Axis, Point3d pt2_Axis, double A)
        {
            //������
            double x0 = pt2_Axis.X - pt1_Axis.X;
            double y0 = pt2_Axis.Y - pt1_Axis.Y;
            double z0 = pt2_Axis.Z - pt1_Axis.Z;
            Vector3 vf = Vector3.Normalize(new Vector3(x0, y0, z0));//��λ������
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
            im = ~m;//����ת��

            RtwMatrix zrot = new RtwMatrix(3, 3);
            zrot[2, 2] = 1;
            zrot[0, 0] = (float)Math.Cos(A);
            zrot[0, 1] = (float)Math.Sin(A);
            zrot[1, 0] = (float)-Math.Sin(A);
            zrot[1, 1] = (float)Math.Cos(A);

            RtwMatrix M, mtemp = new RtwMatrix(3, 3);//�任����
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

            //Tools.WriteMessage("���������"+pt_out.ToString());
            return pt_out;//���������
        }

        /// <summary>
        ///��ģ�����ݽ��б�׼������
        /// </summary>
        public void Normalize()
        {
            //�������ͱ�׼��
            foreach (BSections sec in sections.Values)
            {
                //������ν��浱�û�û������tf2ʱ�Խ������ݽ��б�׼��
                if (sec is SectionDBuser)
                {
                    if (sec.SSHAPE == SecShape.B && (double)sec.SEC_Data[6] == 0
                    && (double)sec.SEC_Data[4] != 0)
                    {
                        sec.SEC_Data[6] = sec.SEC_Data[4];
                    }
                }
                sec.CalculateSecProp();//�����������
                //todo:���������Ϊ���ݿ����ʱ�����н������ת��
            }

            //��׼����������
            foreach (BMaterial mat in mats.Values)
            {
                mat.NormalizeProp();
            }
        }

        /// <summary>
        /// ��Ӻ��������ģ��
        /// </summary>
        /// <param name="com"></param>
        public void AddLoadComb(BLoadComb com)
        {
            _LoadCombTable.Add(com);
        }

        /// <summary>
        /// ����ָ����Ԫ�ĵ�Ԫ�������
        /// </summary>
        /// <param name="com">�������</param>
        /// <param name="iElem">��Ԫ��</param>
        /// <returns>��Ԫ����</returns>
        public  ElemForce CalElemForceComb(BLoadComb com, int iElem)
        {
            ElemForce res = new ElemForce();//Ҫ���صĽ��

            List<BLCFactGroup> comdata = com.LoadCombData;            
            if (com.iTYPE == 0)//���Ϊ�������
            {
                foreach (BLCFactGroup lfg in comdata)
                {
                    ElemForce ef = elemforce[iElem].LCForces[lfg.LCNAME];//��ǰ��ϵ�Ԫ��
                    res=res+ef.Mutiplyby(lfg.FACT);
                }
            }
            else if (com.iTYPE == 1)//���Ϊ+SRSS
            {

            }
            else if (com.iTYPE == 2)//���Ϊ-SRSS
            {

            }
            else if (com.iTYPE==3)//���Ϊƽ��������
            {

            }
            return res;
        }

        /// <summary>
        /// ��������ģ������
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
            _LoadCombTable = new BLoadCombTable();//������ϱ�
            conloads = new SortedList<int, BNLoad>(new RepeatedKeySort());//�ڵ����
            beamloads = new SortedList<int, BBLoad>(new RepeatedKeySort());//����Ԫ����
            selfweight = new SortedList<string, BWeight>();//������Ϣ
            mats = new SortedList<int, BMaterial>();//������Ϣ

            elemforce = new SortedList<int, BElemForceTable>();//��Ԫ������
        }

        /// <summary>
        /// ȡ���ߵ�Ԫ�ĳ���
        /// </summary>
        /// <param name="iEle">�ߵ�Ԫ��</param>
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
        #endregion
        #region model����������ӿڷ���
        /// <summary>
        /// ��ȡmgt�ļ���Ϣ
        /// </summary>
        /// <param name="FilePath">�ļ�·��</param>
        public void ReadFromMgt(string FilePath)
        {
            string currentdata = "notype";//ָ����ǰ��������
            string curLoadCase = "notype";//ָ����ǰ���ع���

            int curSecNUM = 0;//��ǰ�����
            string curSecPOLY = null;//ָʾ��ǰ������
            int IPOLY_num=0;//ָʾ��ǰ�����������������

            //��ʼ��ģ����Ϣ����
            //model = new Bmodel();
            //��ʱ����
            string[] temp, temp1 = null;
            int tempInt = 0;
            double tempDoublt1, tempDoublt2, tempDoublt3 = 0;
            int tempInt1, tempInt2, tempInt3, tempInt4, tempInt5, tempInt6, tempInt7 = 0;

            FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                //�������������ж�
                if (line.StartsWith(";") == true)
                {
                    //��ǰ��Ϊע��
                }
                else if (line.StartsWith("*USE-STLD") == true && line.Contains(","))
                {
                    if (line.Contains(";"))
                    {
                        line = line.Remove(line.IndexOf(';'));//ȥ��ע��
                    }
                    curLoadCase = line;//��ǰ���ع���
                    currentdata = line;//�õ���ǰ��������
                }

                #region ���غ��ض�ȡ
                else if (line.StartsWith("*SELFWEIGHT") && curLoadCase != "notype")
                {
                    temp = line.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);
                    temp1 = curLoadCase.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);//���ع���
                    try
                    {
                        BWeight weightdata = new BWeight();
                        weightdata.Gx = double.Parse(temp[1]);
                        weightdata.Gy = double.Parse(temp[2]);
                        weightdata.Gz = double.Parse(temp[3]);

                        weightdata.LC = temp1[1];//���ع���
                        string str_group = line.Substring(line.LastIndexOf(',') + 1);
                        if (str_group != " ")
                        {
                            weightdata.Group = str_group.Trim();//װ������
                        }

                        selfweight.Add(weightdata.LC, weightdata);
                    }
                    catch
                    {
                        MessageBox.Show("����������Ϣ����\n���Σ�������...", "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion

                else if (line.StartsWith("*") == true)
                {
                    //��������а����ո�Ļ���
                    if (line.IndexOf(' ') > 0)
                    {
                        line = line.Remove(line.IndexOf(' '));
                    }
                    currentdata = line;//�õ���ǰ��������
                }
                #region ģ����Ϣ��ȡ
                else if (line.StartsWith(" ") == true && currentdata == "*UNIT")
                {
                    line.Trim();//�޼���ͷ�ո�
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
                #region �ڵ����ݶ�ȡ
                else if (line.StartsWith(" ") == true && currentdata == "*NODE")
                {
                    //���нڵ����ݶ�ȡ
                    line.Trim();//�޼���ͷ�ո�
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
                        //MessageBox.Show("�����ڵ������ַ�������!");
                    }
                }
                #endregion
                #region ��Ԫ���ݶ�ȡ
                else if (line.StartsWith(" ") == true && currentdata == "*ELEMENT")
                {
                    //���е�Ԫ���ݶ�ȡ
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
                                if (Math.Abs(tempDoublt1) <= 0.0001)//�������Ƕȷǳ�С��������Ϊ�����Ϊ0
                                    tempDoublt1 = 0;
                                elemdata.beta = tempDoublt1;//��¼��Ԫ�����
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
                                elemdata_T.beta = tempDoublt2;//��Ԫ�����
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
                        MessageBox.Show("������Ԫ��Ϣ����!");
                    }
                }
                #endregion
                #region ������Ϣ��ȡ
                else if (line.StartsWith(" ") && currentdata == "*MATERIAL")
                {
                    //���в�����Ϣ�Ķ�ȡ
                    temp = line.Split(',');
                    try
                    {
                        tempInt = int.Parse(temp[0], System.Globalization.NumberStyles.Integer);//���ϱ��
                        MatType tp = (MatType )Enum.Parse(typeof(MatType), temp[1].Trim(), true);//��������
                        BMaterial mat = new BMaterial(tempInt,tp,temp[2].Trim());
                        mat.addMGTdata(line);//�洢ԭʼ����

                        switch (tp)
                        {
                            case MatType .STEEL:
                                mat.setProp(2.06e11,0.3,1.2e-5,7850);
                                break;
                            case MatType .CONC:
                                mat.setProp(3e10,0.2,1e-5,2500);//Ŀǰ��C30����
                                break;
                            case MatType .USER:
                                //mat.setProp(2.06e11, 0.3, 1.2e-5, 7850);
                                break;
                            default:
                                break;
                        }

                        mats.Add(tempInt, mat);//�洢����
                    }
                    catch
                    {
                        MessageBox.Show("����������Ϣ����\n���MIDASģ���õ�ʲô����ϣ���", "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
                #region �������ݶ�ȡ
                else if (line.StartsWith(" ") && currentdata == "*SECTION")//һ�����
                {
                    //���н������Զ�ȡ
                    temp = line.Split(',');
                    try
                    {
                        tempInt = int.Parse(temp[0], System.Globalization.NumberStyles.Number);//������
                        SecType tt = (SecType)Enum.Parse(typeof(SecType), temp[1].Trim(),true);//��������(ö�ٽ���)     
                  
                        #region ������н������ݶ�ȡ 
                        switch (tt)
                        {
                            case SecType.DBUSER:
                                BSections secdata = new SectionDBuser();
                                secdata.Num = tempInt;
                                secdata.TYPE = tt;//��������(ö�ٽ���)                        
                                secdata.Name = temp[2].Trim();//��������

                                secdata.OFFSET[0] = temp[3];//����ƫ��
                                for (int i = 1; i < 7; i++)
                                    secdata.OFFSET[i] = double.Parse(temp[i + 3], System.Globalization.NumberStyles.Number);

                                secdata.bsd = temp[10].Trim() == "YES" ? true : false;
                                secdata.SSHAPE = (SecShape)Enum.Parse(typeof (SecShape), temp[11].Trim(),true);//������״
                                secdata.SEC_Data.Clear();
                                secdata.SEC_Data.Add(int.Parse(temp[12], System.Globalization.NumberStyles.Number));//��������
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

                                sections.Add(tempInt, secdata);//�������
                                break;
                            case SecType.TAPERED:
                                SectionTapered secTapered=new SectionTapered ();
                                secTapered.Num = tempInt;
                                secTapered.TYPE = tt;//��������(ö�ٽ���)                        
                                secTapered.Name = temp[2].Trim();//��������
                                secTapered.OFFSET.Clear();
                                secTapered.OFFSET.Add(temp[3].Trim());//����ƫ��
                                for (int i = 1; i < 9; i++)
                                    secTapered.OFFSET.Add(double.Parse(temp[i + 3], System.Globalization.NumberStyles.Number));

                                secTapered.bsd = temp[12].Trim() == "YES" ? true : false;
                                secTapered.SSHAPE =(SecShape) Enum.Parse(typeof (SecShape), temp[13].Trim(),true);//������״����
                                secTapered.iyVAR =(iVAR)Enum.Parse(typeof(iVAR),temp[14],true);
                                secTapered.izVAR =(iVAR)Enum.Parse(typeof(iVAR), temp[15], true);
                                secTapered.SubTYPE = (STYPE)Enum.Parse(typeof(STYPE), temp[16], true);

                                line = reader.ReadLine();
                                secTapered.SEC_Data.Clear();
                                secTapered.SEC_Data.Add(line.Trim());

                                sections.Add(tempInt, secTapered);//�洢����
                                break;
                            default:
                                break;
                        }
                        #endregion
                        
                    }
                    catch
                    {
                        MessageBox.Show("��������������Գ���\n�Ƿ�ѡ���˲�֧�ֵĽ����������ͣ���", "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #region �Զ�������ȡ
                else if (line.StartsWith(" ") && currentdata == "*SECT-GENERAL")//�Զ������
                {
                    if (line.Contains("SECT="))
                    {
                        SectionGeneral secGEN = new SectionGeneral();
                        temp = line.Trim().Remove(0, 5).Split(',');
                        tempInt = Convert.ToInt16(temp[0].Trim());//������
                        SecType tt = (SecType)Enum.Parse(typeof(SecType), temp[1].Trim(), true);//��������(ö�ٽ���)
                        secGEN.Num = tempInt;
                        curSecNUM = tempInt;//ָ����ǰ����ţ����ڽ���������Ϣ��ȡ
                        curSecPOLY = null;//ָ����ǰ������������
                        IPOLY_num = 0;//ָ��ָ����
                        secGEN.TYPE = tt;
                        secGEN.Name = temp[2].Trim();
                        secGEN.OFFSET.Add(temp[3].Trim());
                        for (int i = 1; i < 7; i++)
                            secGEN.OFFSET.Add( double.Parse(temp[i + 3], System.Globalization.NumberStyles.Number));

                        secGEN.bsd = temp[10].Trim() == "YES" ? true : false;
                        secGEN.SSHAPE = (SecShape)Enum.Parse(typeof(SecShape), temp[11].Trim(), true);//������״����
                        secGEN.bBU = temp[12].Trim() == "YES" ? true : false;
                        secGEN.bEQ = temp[13].Trim() == "YES" ? true : false;

                        line = reader.ReadLine();//�ڶ���
                        temp = line.Split(',');
                        secGEN.setSecProp1(double.Parse(temp[0]),double .Parse(temp[1]),double.Parse(temp[2]),
                            double .Parse(temp[3]),double.Parse(temp[4]),double.Parse(temp[5]));
                        line = reader.ReadLine();//������
                        temp = line.Split(',');
                        secGEN.setSecProp2(double.Parse(temp[0]), double.Parse(temp[1]), double.Parse(temp[2]),
                            double.Parse(temp[3]), double.Parse(temp[4]), double.Parse(temp[5]),
                            double.Parse(temp[6]), double.Parse(temp[7]), double.Parse(temp[8]),
                            double.Parse(temp[9]));
                        line = reader.ReadLine();//������
                        temp = line.Split(',');
                        secGEN.setSecProp3(double.Parse(temp[0]), double.Parse(temp[1]), double.Parse(temp[2]),
                            double.Parse(temp[3]), double.Parse(temp[4]), double.Parse(temp[5]),
                            double.Parse(temp[6]),double.Parse(temp[7]));

                        sections.Add(tempInt,secGEN);//�����������ģ�Ͷ���
                    }
                    else if (line.Contains("OPOLY="))//����OPOLY=����
                    {
                        curSecPOLY = "OPOLY";//ָʾ��ǰ��Է���
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
                            sections.Remove(curSecNUM);//ɾ�����н���
                            sections.Add(curSecNUM, SGtemp);//�����µĽ���
                        }
                    }
                    else if (line.Contains("IPOLY="))//����IPOLY=����
                    {
                        curSecPOLY = "IPOLY";//ָʾ��ǰ��Է���
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
                            sections.Remove(curSecNUM);//ɾ�����н���
                            sections.Add(curSecNUM, SGtemp);//�����µĽ���
                        }
                    }
                    else if (curSecPOLY == "OPOLY")//������OPOLY=����
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
                            sections.Remove(curSecNUM);//ɾ�����н���
                            sections.Add(curSecNUM, SGtemp);//�����µĽ���
                        }
                    }
                    else if (curSecPOLY=="IPOLY")//������IPOLY=����
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
                            sections.Remove(curSecNUM);//ɾ�����н���
                            sections.Add(curSecNUM, SGtemp);//�����µĽ���
                        }
                    }
                }
                #endregion
                #endregion
                #region �嵥Ԫ������ݶ�ȡ
                else if (line.StartsWith(" ") == true && currentdata == "*THICKNESS")
                {
                    temp = line.Split(',');
                    try
                    {
                        tempInt = int.Parse(temp[0], System.Globalization.NumberStyles.Number);//��ȱ��
                        BThickness thidata = new BThickness();
                        thidata.iTHK = tempInt;
                        thidata.TYPE = temp[1].Trim();
                        thidata.bSAME = true;
                        if (temp[2].Trim() == "NO")
                            thidata.bSAME = false;
                        thidata.THIK_IN = double.Parse(temp[3].Trim(), System.Globalization.NumberStyles.Float);
                        thidata.THIK_OUT = double.Parse(temp[4].Trim(), System.Globalization.NumberStyles.Float);

                        thickness.Add(tempInt, thidata);//�������
                    }
                    catch
                    {
                        MessageBox.Show("���������ȳ���", "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
                #region �߽��������ݶ�ȡ
                else if (line.StartsWith(" ") && currentdata == "*CONSTRAINT")
                {
                    //���б߽�������ȡ
                    BConstraint support = new BConstraint();
                    temp = line.Split(',');
                    if (temp[0].Trim().Contains(" "))
                    {
                        temp1 = temp[0].Trim().Split(' ');//�ڵ��б�
                        support.node_list.AddRange(temp1);
                    }
                    else
                    {
                        support.node_list.Add(temp[0]);
                    }

                    //��ȡԼ�����
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

                    constraint.Add(support);//��ӵ�ģ�����ݿ���

                }
                #endregion
                #region �����б����ݶ�ȡ
                else if (line.StartsWith(" ") == true && currentdata == "*STLDCASE")
                {
                    //����ַ���
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

                        STLDCASE.Add(lcdata);//��ӵ�ģ�����ݿ�
                    }
                    catch
                    {
                        MessageBox.Show("���������б����\n���Σ��ҿ���...", "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion

                #region �ڵ�����б��ȡ
                else if (line.StartsWith(" ") == true && currentdata == "*CONLOAD")
                {
                    //����ַ�
                    temp = curLoadCase.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);//temp[1]Ϊ������
                    temp1 = line.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);//temp1Ϊ������

                    try
                    {
                        BNLoad BNLoaddata = new BNLoad(int.Parse(temp1[0]));
                        BNLoaddata.FX = double.Parse(temp1[1]);
                        BNLoaddata.FY = double.Parse(temp1[2]);
                        BNLoaddata.FZ = double.Parse(temp1[3]);
                        BNLoaddata.MX = double.Parse(temp1[4]);
                        BNLoaddata.MY = double.Parse(temp1[5]);
                        BNLoaddata.MZ = double.Parse(temp1[6]);

                        BNLoaddata.LC = temp[1];//��������
                        string str_group = line.Substring(line.LastIndexOf(',') + 1);
                        if (str_group != " ")
                        {
                            BNLoaddata.Group = str_group.Trim();//װ������
                        }

                        conloads.Add(BNLoaddata.iNode, BNLoaddata);//����ģ�����ݿ�
                    }
                    catch
                    {
                        MessageBox.Show("�����ڵ�����б����\n���Σ��ҿ���...", "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
                #region ����Ԫ�����б��ȡ
                else if (line.StartsWith(" ") == true && currentdata == "*BEAMLOAD")
                {
                    //����ַ�
                    temp = curLoadCase.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);//temp[1]Ϊ������
                    temp1 = line.Split(new char[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries);//temp1Ϊ������
                    try
                    {
                        BBLoad BeamLoadData = new BBLoad();
                        BeamLoadData.ELEM_num = int.Parse(temp1[0].Trim());
                        BeamLoadData.CMD = temp1[1].Trim();
                        BeamLoadData.TYPE = temp1[2].Trim();
                        BeamLoadData.setLoadDir(temp1[3].Trim());
                        if (temp1[4].Trim() == "YES")
                            BeamLoadData.bPROJ = true;

                        //����ƫ������
                        BeamLoadData.readEccenDataMgt(line);

                        double dd1 = double.Parse(temp1[10].Trim());
                        double pp1 = double.Parse(temp1[11].Trim());
                        double dd2 = double.Parse(temp1[12].Trim());
                        double pp2 = double.Parse(temp1[13].Trim());
                        double dd3 = double.Parse(temp1[14].Trim());
                        double pp3 = double.Parse(temp1[15].Trim());
                        double dd4 = double.Parse(temp1[16].Trim());
                        double pp4 = double.Parse(temp1[17].Trim());
                        BeamLoadData.setLoadData(dd1, pp1, dd2, pp2, dd3, pp3, dd4, pp4);//���º�������

                        BeamLoadData.LC = temp[1];//����
                        if (temp1[18] != " ")
                        {
                            BeamLoadData.Group = temp1[18].Trim();//װ������
                        }

                        beamloads.Add(BeamLoadData.ELEM_num, BeamLoadData);//����ģ�����ݿ�
                    }
                    catch
                    {
                        MessageBox.Show("��������Ԫ�����б����\n���Σ��ҿ���...", "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
            }
            reader.Close();
            #region �ٴδ��ļ�����ȡ
            FileStream str = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(str,Encoding.Default);//��ϵͳĬ�ϱ����
            ReadLoadComb(ref sr);   //��ȡ�������
            sr.Close();
            #endregion

            Normalize();//ģ�ͱ�׼������
            GenBeamKpoint();//����ģ��������Ԫ�Ľڵ㷽�����Ϣ
        }

        /// <summary>
        /// ��ȡ��������б�
        /// </summary>
        /// <param name="srt">�ļ���</param>
        public void ReadLoadComb(ref StreamReader srt)
        {
            /* 1��׼��*/
            bool bRead = false;                     //�Ƿ���Զ�ȡ
            String strText = null;                  //��ǰ���ı�
            String strStartFlag = "*LOADCOMB";      //���ݿ�ʼ��־
            String strEndFlag = "";                 //���ݽ�����־
            char szSplit = ',';                     //���ݷָ���

            /* 2��ѭ����ȡ*/
            string curName = null;                  //��ǰ�����������
            for (strText = srt.ReadLine(); strText != null; strText = srt.ReadLine())
            {
                /* 2.1���ж��Ƿ�������ݡ������������ñ�־��������һ��ѭ����ʼ��ȡ����û�ж���������������һ���жϡ�*/
                /* 2.2��bRead=true����ʾ�Ѿ����Զ������ˡ�����ʱ��Ҫ�ж��Ƿ��Ѿ��������ݡ�*/
                if (!bRead)
                {
                    if (strText.StartsWith(strStartFlag))
                    {
                        bRead = true;
                    }
                    continue;
                }
                else if (strText.StartsWith(";"))//���Ϊע�������
                    continue;
                else if (strText.CompareTo(strEndFlag) == 0)
                    return;
                else if (strText.Trim().StartsWith("NAME"))
                {   
                    /*���뵱ǰ������ϻ������ݶ�ȡ*/
                    string[] sArrayCur=strText.Trim().Split(szSplit);
                    string sName=sArrayCur[0].Substring(sArrayCur[0].IndexOf('=')+1).Trim();//�������
                    LCKind kind=LCKind.GEN;//�������
                    bool isActive=true;//�Ƿ񼤻�
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
                    BLoadComb blc = new BLoadComb();        //��ǰ�����������
                    blc.SetData1(sName, kind, isActive, bEs, Type, sArrayCur[5].Trim());
                    curName = sName;//��¼��ǰ����
                    _LoadCombTable.Add(blc);
                    continue;
                }
                else if (strText.StartsWith(" ")&&strText.Contains(szSplit.ToString()))
                {
                    /*���뵱ǰ������Ϲ��������*/
                    //BLoadComb tempBLC = LOADCOMBS[curName];
                    BLoadComb tempBLC = _LoadCombTable[curName];//ȡ����ǰ���
                    //LOADCOMBS.Remove(curName);
                    _LoadCombTable.Remove(curName);//����ϱ���ɾ��
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
                            default: lcfg.ANAL = ANAL.ST; break;
                        }
                        lcfg.LCNAME = sArrayCur[i + 1].Trim();
                        lcfg.FACT = Convert.ToDouble(sArrayCur[i + 2]);
                        tempBLC.AddLCFactGroup(lcfg);                        
                    }
                    _LoadCombTable.Add(tempBLC);//����ӵ���ϱ���
                    //_LoadCombs.Add(tempBLC.NAME, tempBLC);
                }
                else
                    continue;
            }
        }

        /// <summary>
        /// д��ANSYS�������ļ�
        /// </summary>
        /// <param name="inp">д���ļ�·��</param>
        /// <param name="BeamType">����Ԫ���ͣ�1��ʾbeam44��2��ʾbeam188��3��ʾbeam189</param>
        public bool WriteToInp(string inp, int BeamType)
        {
            FileStream stream = File.Open(inp, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("FINISH");
            writer.WriteLine("/CLEAR");
            writer.WriteLine("/COM,Midas2Ansys INP File Created at " + System.DateTime.Now);
            writer.WriteLine("/COM,*******http://www.lubanren.com********");

            writer.WriteLine("\n/PREP7");
            writer.WriteLine("\n!��Ԫ������Ϣ...");
            switch (BeamType)//����ѡ�������Ԫ���Ͷ�������
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
                    //writer.WriteLine("et,1,188");
                    break;
            }
            //�嵥Ԫ��������
            writer.WriteLine("et,2,181");
            //��ܡ�ֻ������ֻ��ѹ��Ԫ��������
            writer.WriteLine("et,3,180");

            //ʵ������Ϣ
            writer.WriteLine("\n!ʵ������Ϣ����...");
            foreach (KeyValuePair<int, BThickness> thi in this.thickness)//���������Ϣ�γ�ʵ����
            {
                writer.WriteLine("r,{0},{1}", thi.Key.ToString(), thi.Value.THIK_IN.ToString());
            }
            #region LINK180��Ԫʵ����������
            List<int> TRU_sec = new List<int>();//��ʱ��¼������
            List <int> TEN_sec=new List<int> ();
            List <int> COM_sec=new List<int> ();
            foreach (KeyValuePair<int, Element> elem in this.elements)// ������Ԫ��Ϣ�γ�ʵ������link180��Ԫ�ã�
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


            writer.WriteLine("\n!������Ϣ����...");
            foreach (KeyValuePair<int, BSections> sec in this.sections)
            {
                writer.WriteLine(sec.Value.WriteData());
            }

            #region ������Ϣ���
            writer.WriteLine("\n!������Ϣ����...");
            foreach (KeyValuePair<int, BMaterial> mat in this.mats)
            {
                writer.WriteLine("mp,ex,{0},{1}", mat.Value.iMAT.ToString("G"), mat.Value.Elast.ToString("G"));
                writer.WriteLine("mp,prxy,{0},{1}", mat.Value.iMAT.ToString("G"), mat.Value.Poisn.ToString("G"));
                writer.WriteLine("mp,dens,{0},{1}", mat.Value.iMAT.ToString("G"), mat.Value.Den.ToString("G"));
                writer.WriteLine("mp,alpx,{0},{1}", mat.Value.iMAT.ToString("G"), mat.Value.Thermal.ToString("G"));
            }
            #endregion
            writer.WriteLine("\n!�ڵ�������Ϣ");
            //����ڵ���Ϣ
            foreach (KeyValuePair<int, Bnodes> node in this.nodes)
            {
                writer.WriteLine("n," + node.Key.ToString("0") + "," + node.Value.X.ToString() + "," + node.Value.Y.ToString() + "," + node.Value.Z
                    .ToString());
            }
            //�����Ԫ��Ϣ
            writer.WriteLine("\n!��Ԫ������Ϣ");
            foreach (KeyValuePair<int, Element> elem in this.elements)
            {
                //����Ԫ���ͷ������
                switch (elem.Value.TYPE)
                {
                    case ElemType.BEAM:
                        writer.WriteLine("type,1");
                        writer.WriteLine("mat,{0}", elem.Value.iMAT.ToString());
                        writer.WriteLine("secnum," + elem.Value.iPRO.ToString());
                        writer.WriteLine("en," + elem.Value.NodeString());
                        break;
                    case ElemType.TRUSS:
                        writer.WriteLine("type,3");
                        writer.WriteLine("mat,{0}", elem.Value.iMAT.ToString());
                        writer.WriteLine("real,{0}",elem.Value.iPRO+100);
                        writer.WriteLine("en,{0}",elem.Value.NodeString());
                        break;
                    case ElemType.PLATE:
                        //writer.WriteLine("!{0}�ŵ�Ԫ��ƽ�浥Ԫ", elem.Value.iEL.ToString());
                        writer.WriteLine("real,{0}", elem.Value.iPRO.ToString());//��Ⱥ�
                        writer.WriteLine("type,2");
                        writer.WriteLine("mat,{0}", elem.Value.iMAT.ToString());
                        writer.WriteLine("en," + elem.Value.NodeString());
                        break;
                    case ElemType.TENSTR:
                        //writer.WriteLine("!{0}�ŵ�Ԫ��ֻ����Ԫ", elem.Value.iEL.ToString());
                        writer.WriteLine("type,3");
                        writer.WriteLine("mat,{0}", elem.Value.iMAT.ToString());
                        writer.WriteLine("real,{0}", elem.Value.iPRO + 200);
                        writer.WriteLine("en,{0}", elem.Value.NodeString());
                        break;
                    case ElemType.COMPTR:
                        //writer.WriteLine("!{0}�ŵ�Ԫ��ֻѹ��Ԫ", elem.Value.iEL.ToString());
                        writer.WriteLine("type,3");
                        writer.WriteLine("mat,{0}", elem.Value.iMAT.ToString());
                        writer.WriteLine("real,{0}", elem.Value.iPRO + 300);
                        writer.WriteLine("en,{0}", elem.Value.NodeString());
                        break;
                    default:
                        break;
                }

            }
            //�������ģ��
            writer.WriteLine("\n/SOLU");
            writer.WriteLine("\n!Լ������");
            #region �߽��������
            foreach (BConstraint nodesuport in this.constraint)
            {
                foreach (string nodeslist in nodesuport.node_list)
                {
                    //apdl�����ʽ���£�
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
            #region �������
            writer.WriteLine("\n!ʩ��ģ�ͺ���");
            foreach (BLoadCase lc in this.STLDCASE)
            {
                writer.WriteLine("\n!����{0}", lc.LCName);
                writer.WriteLine("*create,LC_{0},lc", lc.LCName);
                #region ������غ���
                foreach (KeyValuePair<string, BWeight> weightdata in this.selfweight)
                {
                    if (weightdata.Value.LC == lc.LCName)
                    {
                        //ע��ANSYS�м��ٶȷ��򷴺�
                        writer.WriteLine("acel,{0},{1},{2}",
                            (-weightdata.Value.ACELx).ToString(),
                            (-weightdata.Value.ACELy).ToString(),
                            (-weightdata.Value.ACELz).ToString());
                    }
                }
                #endregion
                #region ����ڵ����
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
                #region �������Ԫ����
                foreach (KeyValuePair<int, BBLoad> bload in this.beamloads)
                {
                    if (bload.Value.LC == lc.LCName)
                    {
                        //to do:�����Ԫ������Ϣ
                        if (bload.Value.TYPE == "UNIMOMENT" ||
                            bload.Value.TYPE == "CONMOMENT" ||
                            bload.Value.getP(3) != 0)
                        {
                            writer.WriteLine("!��Ԫ({0})��ANSYS����Ҫ��Ԫϸ��...", bload.Key.ToString());
                        }
                        else if (bload.Value.TYPE == "UNILOAD")
                        {
                            switch (bload.Value.Dir)
                            {
                                case DIR.GX:
                                    writer.WriteLine("!��Ԫ({0})�ĵ�Ԫ����ʩ������ϵ���Ǿֲ�����",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GY:
                                    writer.WriteLine("!��Ԫ({0})�ĵ�Ԫ����ʩ������ϵ���Ǿֲ�����",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GZ:
                                    writer.WriteLine("!��Ԫ({0})�ĵ�Ԫ����ʩ������ϵ���Ǿֲ�����",
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
                                    writer.WriteLine("!��Ԫ({0})�ĵ�Ԫ����ʩ������ϵ���Ǿֲ�����",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GY:
                                    writer.WriteLine("!��Ԫ({0})�ĵ�Ԫ����ʩ������ϵ���Ǿֲ�����",
                                        bload.Key.ToString());
                                    break;
                                case DIR.GZ:
                                    writer.WriteLine("!��Ԫ({0})�ĵ�Ԫ����ʩ������ϵ���Ǿֲ�����",
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

        /// <summary>
        /// ��ȡMIDAS��������������ģ��
        /// </summary>
        /// <param name="MidasFile">MIDAS����ĵ�Ԫ��������λĬ��ΪkN,m</param>
        public void ReadElemForces(string MidasForceFile)
        {
            string line = null;//���ı�
            string[] curdata= null;//��ǰ���ݱ�洢����
            int curNum=0;//��ǰ��Ԫ��
            string curLC=null;//��ǰ������
            double[] tempDouble =new double[6];

            int i = 0;
            
            FileStream stream = File.Open(MidasForceFile, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            line = reader.ReadLine();
            
            for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                curdata = line.Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);//�ַ����ָ�
                curNum = int.Parse(curdata[0], System.Globalization.NumberStyles.Number);//��ǰ��Ԫ��
                curLC=curdata[1];//��ǰ��������

                 //ȡ����������
                for (int k = 0; k < 6; k++)
                {
                    tempDouble[k] = double.Parse(curdata[k+3], System.Globalization.NumberStyles.Float);
                }
                //������������
                SecForce sec1 = new SecForce(tempDouble[0],tempDouble[3],tempDouble[1],
                    tempDouble[2],tempDouble[4],tempDouble[5]);

                #region ��ģ�����ݽṹ�����
                if (this.elemforce.ContainsKey(curNum))//������е�ǰ��Ԫ
                {
                    if (this.elemforce[curNum].hasLC(curLC))//������е�ǰ���
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

                        this.elemforce[curNum].LCForces = tempEF;//���صĵ�ģ�����ݿ���
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
        #endregion
    }

    #region ���û�����������
    /// <summary>
    /// 3d����
    /// </summary>
    public class Point3d : Object
    {
        private double XX, YY, ZZ;
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public Point3d()
        {
            XX = 0;
            YY = 0;
            ZZ = 0;
        }
        /// <summary>
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="nx">�ڵ�X����</param>
        /// <param name="ny">�ڵ�Y����</param>
        /// <param name="nz">�ڵ�Z����</param>
        public Point3d(double nx, double ny, double nz)
        {
            XX = nx;
            YY = ny;
            ZZ = nz;
        }
        //����
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
        //��������
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
    /// 2d����
    /// </summary>
    [Serializable]
    public class Point2d : Object
    {
        private double XX, YY;
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public Point2d()
        {
            XX = 0;
            YY = 0;
        }
        /// <summary>
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="nx">�ڵ�X����</param>
        /// <param name="ny">�ڵ�Y����</param>
        public Point2d(double nx, double ny)
        {
            XX = nx;
            YY = ny;
        }
        //����
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
    }

    /// <summary>
    /// ��ά�㼯��
    /// </summary>
    [Serializable]
    public class Point2dCollection:Object
    {
        private List<Point2d> _pts;

        //����
        /// <summary>
        /// �㼯
        /// </summary>
        public List<Point2d> Pts
        {
            get { return _pts; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public int Length
        {
            get { return _pts.Count; }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public Point2dCollection()
        {
            _pts = new List<Point2d>();
        }

        /// <summary>
        /// ��ӵ��뼯��
        /// </summary>
        /// <param name="pt">������</param>
        public void addPt(Point2d pt)
        {
            _pts.Add(pt);
        }
    }

    /// <summary>
    /// ʵ��hash���ظ�����Ա�����
    /// </summary>
    [Serializable]
    public class RepeatedKeySort : IComparer<int>
    {
        #region IComparer ��Ա
        public int Compare(int x, int y)
        {
            //return -1;//ֱ�ӷ��ز���������
            //���´����ʵ���Զ�����
            int iResult = x - y;
            if (iResult == 0) iResult = -1;
            return iResult;
        }
        #endregion
    }
    /// <summary>
    /// ʵ��SortedList���Զ�������
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
            //����
            // int iResult = (int)x - (int)y;
            // if(iResult == 0) iResult = -1;
            // return iResult;
        }
    }
    #endregion 
}
