using ExpressionMap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExpressionMap4NetTest
{
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            DtoA dtoa = new DtoA() { F2 = 1, P1 = "P1", P2 = "P2" };
            Map<DtoA, DtoB>.Instance
                .DefineRule(x => x.F1, from => from.F1 + from.F2)
                .DefineRule(x => x.P1, from => Convert.ToString(from.F2))
                .CreatRule();
            Map<DtoA, DtoC>.Instance
                .DefineRule(x => x.F1, from => from.F1 + from.F2)
                .DefineRule(x => x.P1, from => Convert.ToString(from.F2))
                .DefineRule(x => x.P3, from => Tran(from.F2))
                .CreatRule();
            var dtoB = Map<DtoA, DtoB>.Instance.Trans(dtoa);
            var dtoC = Map<DtoA, DtoC>.Instance.Trans(dtoa);
        }
        public string Tran(int t)
        {
            return t.ToString() + 100;
        }
    }


    public class DtoA
    {
        public string F1;
        public int F2;
        public string P1 { get; set; }
        public string P2 { get; set; }
    }
    public class DtoB
    {

        public string F1;
        public string F3;
        public string P1 { get; set; }
        public string P3 { get; set; }
    }
    public class DtoC
    {

        public string F1;
        public string F3;
        public string P1 { get; set; }
        public string P3 { get; set; }
    }
}
