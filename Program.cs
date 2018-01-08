using Mapper.Exprsssion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //var obj1 = new { Prop1 = "obj1_1", Prop2 = "obj1_2" };
            //var obj2 = new { Prop1 = "obj2_1", Prop2 = "obj2_2", Prop3 = "obj2_3" };

            //var obj3 = new { Prop1 = obj1.Prop1, Prop2 = obj2.Prop2, Prop3 = "obj3_3" };
            //var obj4 = new { Prop1 = obj1.Prop1, Prop2 = obj1.Prop1 + obj2.Prop2, Prop3 = "obj3_3" };

            //Expression<Func<DtoA, DtoB>> t0 = (a) =>
            //new DtoB() { P1 = a.F1 };


            //Expression<Func<DtoA, string>> t = (a) =>
            //Convert.ToString(Convert.ToString(a.P1) + "b");
            //var newExpression = MapExpressionVisitor.ParseToBody(t,typeof(DtoA));




            DtoA dtoa = new DtoA() { F2 = 1, P1 = "P1", P2 = "P2" };
            Map<DtoA, DtoB>.Instance
                .DefineRule(x => x.F1, x => x.F1 + x.F2)
                .DefineRule(x => x.P1, from => Convert.ToString(from.F2))
                .CreatRule();
            Map<DtoA, DtoC>.Instance
                .DefineRule(x => x.F1, x => x.F1 + x.F2)
                .DefineRule(x => x.P1, from => Convert.ToString(from.F2))
                .CreatRule();
            var dtoC = Map<DtoA, DtoC>.Instance.Trans(dtoa);
            var dtoB = Map<DtoA, DtoB>.Instance.Trans(dtoa);

        }
    }

    #region dto

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
    #endregion
}
