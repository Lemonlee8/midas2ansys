using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace MidasGenModel.model
{
    #region Model Info(ģ��������)

    /// <summary>
    /// ģ�͵�λ��Ϣ
    /// </summary>
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
    /// ������
    /// </summary>
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
    }

    /// <summary>
    /// ��Ԫ����
    /// </summary>
    public abstract class Element : Object
    {
        private int _iEL, _iMAT, _iPRO;
        private string _TYPE;
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
        public string TYPE
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
            TYPE = "NOTYPE";
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
    public class FrameElement : Element
    {
        /// <summary>
        /// ��Ԫ����ǣ��ȣ�
        /// </summary>
        private double Angle;
        private int _iSUB;
        private double _EXVAL;

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
        /// ��������1
        /// </summary>
        public int iSUB
        {
            set { _iSUB = value; }
            get { return _iSUB; }
        }
        /// <summary>
        /// ��������2
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
            this.TYPE = "BEAM";
            this.beta = 0;
        }
        /// <summary>
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="num">��Ԫ��</param>
        /// <param name="type">��Ԫ����</param>
        /// <param name="mat">���Ϻ�</param>
        /// <param name="pro">�������Ժ�</param>
        /// <param name="iNodes">�ڵ������</param>
        public FrameElement(int num, string type, int mat, int pro, params int[] iNodes)
            : base(num, type, mat, pro, iNodes)
        {
            //���û���Ĺ��캯��
        }
    }

    /// <summary>
    /// ƽ�浥Ԫ��
    /// </summary>
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
            this.TYPE = "PLATE";
        }

        /// <summary>
        /// ���û���Ĺ��캯��
        /// </summary>
        /// <param name="num">��Ԫ��</param>
        /// <param name="type">��Ԫ����</param>
        /// <param name="mat">���Ϻ�</param>
        /// <param name="pro">����ţ���Ⱥ�</param>
        /// <param name="iNodes">�ڵ������</param>
        public PlanarElement(int num, string type, int mat, int pro, params int[] iNodes)
            : base(num, type, mat, pro, iNodes)
        {
            //���û���Ķ�Ӧ���캯��
        }
    }

    /// <summary>
    /// �洢�������Ե���
    /// </summary>
    public class BSections
    {
        /// <summary>
        /// �����
        /// </summary>
        private int iSEC;
        /// <summary>
        /// �������ͣ�DBUSER��VALU
        /// </summary>
        public string TYPE;
        /// <summary>
        /// ��������
        /// </summary>
        private string SNAME;
        /// <summary>
        /// ����ƫ������
        /// </summary>
        public ArrayList OFFSET;
        /// <summary>
        /// ����ƫ������
        /// </summary>
        public bool bsd;
        /// <summary>
        /// ������״��B��ʾ����
        /// </summary>
        public string SSHAPE;
        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public ArrayList SEC_Data;

        //����
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
        /// ��һ����ʽ�������������Ϣ
        /// </summary>
        /// <returns></returns>
        public string WriteData()
        {
            string res = null;
            if (this.SSHAPE.Trim() == "B" && (int)this.SEC_Data[0] == 2)//���ν���
            {
                res += "sectype," + this.Num.ToString() + ",beam,hrec," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString() + "," + SEC_Data[3].ToString() + "," + SEC_Data[3].ToString()
                + "," + SEC_Data[6].ToString() + "," + SEC_Data[4].ToString();
            }
            else if (this.SSHAPE.Trim() == "H" && (int)this.SEC_Data[0] == 2)//H�ͽ���
            {
                res += "sectype," + this.Num.ToString() + ",beam,i," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString() + "," + SEC_Data[4].ToString()
                    + "," + SEC_Data[4].ToString() + "," + SEC_Data[3].ToString();
            }
            else if (this.SSHAPE.Trim() == "P" && (int)this.SEC_Data[0] == 2)//Բ�ܽ���
            {
                double ri = (double)SEC_Data[1] / 2 - (double)SEC_Data[2];
                double ro = (double)SEC_Data[1] / 2;
                res += "sectype," + this.Num.ToString() + ",beam,ctube," + this.Name;
                res += "\nsecdata," + ri.ToString() + "," + ro.ToString();
            }
            else if (this.SSHAPE.Trim() == "SB" && (int)this.SEC_Data[0] == 2)//���ν���
            {
                res += "sectype," + this.Num.ToString() + ",beam,rect," + this.Name;
                res += "\nsecdata," + SEC_Data[2].ToString() + "," + SEC_Data[1].ToString();
            }
            else if (this.SSHAPE.Trim() == "T" && (int)this.SEC_Data[0] == 2)//T�ͽ���
            {
                res += "sectype," + this.Num.ToString() + ",beamr,t," + this.Name;
                res += "\nsecdata," + SEC_Data[1].ToString() + "," + SEC_Data[2].ToString() + "," + SEC_Data[3].ToString() + "," +
                    SEC_Data[4].ToString();
            }
            else if (this.SSHAPE.Trim() == "SR" && (int)this.SEC_Data[0] == 2)//Բ�ν���
            {
                res += "sectype," + this.Num.ToString() + ",beamr,csolid," + this.Name;
                res += "\nsecdata," + ((double)SEC_Data[1] / 2).ToString();
            }
            else
            {
                res += "!���½�����״��Ϣδ����" + SSHAPE;
            }
            return res;
        }
    }

    /// <summary>
    /// �洢�嵥Ԫ�����Ϣ����
    /// </summary>
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
    #endregion

    #region Constraint (�߽�Լ����)
    /// <summary>
    /// �߽�������
    /// </summary>
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
    #endregion

    /// <summary>
    /// ģ���ࣺ��װ����������Ϣ
    /// </summary>
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
        #endregion

        /// <summary>
        /// ��ʼ��ģ������
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
            conloads = new SortedList<int, BNLoad>(new RepeatedKeySort());//�ڵ����
            beamloads = new SortedList<int, BBLoad>(new RepeatedKeySort());//����Ԫ����
            selfweight = new SortedList<string, BWeight>();//������Ϣ
        }
        /// <summary>
        /// ת������Ԫ�ؼ�����Ϣ
        /// </summary>
        public void GenBeamKpoint()
        {
            int Nnodes = 9999;//ģ�ͽڵ������������ڷ�������ʼ���
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

        #region model�෽��
        /// <summary>
        ///��ģ�����ݽ��б�׼������
        /// </summary>
        public void Normalize()
        {
            //�������ͱ�׼��
            foreach (BSections sec in sections.Values)
            {
                //������ν��浱�û�û������tf2ʱ�Խ������ݽ��б�׼��
                if (sec.SSHAPE.Trim() == "B" && (double)sec.SEC_Data[6] == 0
                    && (double)sec.SEC_Data[4] != 0)
                {
                    sec.SEC_Data[6] = sec.SEC_Data[4];
                }
                //todo:���������Ϊ���ݿ����ʱ�����н������ת��
            }
        }

        /// <summary>
        /// ��ȡmgt�ļ���Ϣ
        /// </summary>
        /// <param name="FilePath">�ļ�·��</param>
        public void ReadFromMgt(string FilePath)
        {
            string currentdata = "notype";//ָ����ǰ��������
            string curLoadCase = "notype";//ָ����ǰ���ع���
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


                        switch (temp[1].Trim().ToUpper())
                        {
                            case "BEAM":
                                tempDoublt1 = double.Parse(temp[6], System.Globalization.NumberStyles.Number);
                                FrameElement elemdata = new FrameElement(
                                    tempInt, "BEAM", tempInt1, tempInt2, tempInt3, tempInt4);
                                elemdata.beta = tempDoublt1;//��¼��Ԫ�����
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
                #region �������ݶ�ȡ
                else if (line.StartsWith(" ") == true && currentdata == "*SECTION")
                {
                    //���н������Զ�ȡ
                    temp = line.Split(',');
                    try
                    {
                        tempInt = int.Parse(temp[0], System.Globalization.NumberStyles.Number);//������

                        BSections secdata = new BSections();
                        secdata.Num = tempInt;
                        secdata.TYPE = temp[1];//��������
                        secdata.Name = temp[2];//��������
                        secdata.OFFSET[0] = temp[3];//����ƫ��
                        for (int i = 1; i < 7; i++)
                            secdata.OFFSET[i] = double.Parse(temp[i + 3], System.Globalization.NumberStyles.Number);

                        secdata.SSHAPE = temp[11];//������״
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
                    }
                    catch
                    {
                        MessageBox.Show("�����������Գ���\n�Ƿ�ѡ���˲�֧�ֵĽ����������ͣ���", "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

            Normalize();//ģ�ͱ�׼������
            GenBeamKpoint();//����ģ��������Ԫ�Ľڵ㷽�����Ϣ
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
                    writer.WriteLine("et,1,188");
                    break;
            }
            //�嵥Ԫ��������
            writer.WriteLine("et,2,43");

            //ʵ������Ϣ
            foreach (KeyValuePair<int, BThickness> thi in this.thickness)
            {
                writer.WriteLine("r,{0},{1}", thi.Key.ToString(), thi.Value.THIK_IN.ToString());
            }

            writer.WriteLine("\n!������Ϣ����...");
            foreach (KeyValuePair<int, BSections> sec in this.sections)
            {
                writer.WriteLine(sec.Value.WriteData());
            }

            writer.WriteLine("\n!������Ϣ����...");
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
                    case "BEAM":
                        writer.WriteLine("type,1");
                        writer.WriteLine("secnum," + elem.Value.iPRO.ToString());
                        writer.WriteLine("en," + elem.Value.NodeString());
                        break;
                    case "PLATE":
                        //writer.WriteLine("!{0}�ŵ�Ԫ��ƽ�浥Ԫ", elem.Value.iEL.ToString());
                        writer.WriteLine("real,{0}", elem.Value.iPRO.ToString());//��Ⱥ�
                        writer.WriteLine("type,2");
                        writer.WriteLine("en," + elem.Value.NodeString());
                        break;
                    case "TENSTR":
                        writer.WriteLine("!{0}�ŵ�Ԫ��ֻ����Ԫ", elem.Value.iEL.ToString());
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
        #endregion
    }

    /// <summary>
    /// ����
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
    /// ʵ��hash���ظ�����Ա�����
    /// </summary>
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
}
