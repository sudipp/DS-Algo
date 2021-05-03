using System;
using System.Collections.Generic;

namespace ConsoleApplication1.MicrosoftFolder
{
    public class Evaluator
    {
        Extractor extractor = null;
        Stack<Exression> stack = null;
        public Evaluator()
        {
            stack = new Stack<Exression>();
            extractor = new Extractor();
        }

        public string Evaluate(string expression)
        {
            foreach (Exression exp in extractor.Extract(expression))
                stack.Push(exp);

            Exression result = EvaluateExpression();
            return result.Format();
        }

        private Exression EvaluateExpression()
        {
            return null;
        }
    }

    abstract class Exression
    {
        public string Value;
        public abstract string Format();
    }

    class Operator : Exression
    {
        public Operator(string name) { this.Value = name; }

        public override string Format()
        {
            return string.Format("{0}", this.Value);
        }
    }

    class Whole : Exression
    {
        public Whole(string val) { this.Value = val; }

        public override string Format()
        {
            return string.Format("{0}", this.Value);
        }
    }
    class Franction : Exression
    {
        public int Numerator;
        public int Denominator;
        public Franction(string val) {
            this.Value = val;

            string[] arr = this.Value.Split('/');
            if (arr.Length > 0)
            {
                Numerator = int.Parse(arr[0]);
                Denominator = int.Parse(arr[1]);
            }
        }

        public override string Format()
        {
            return string.Format("{0}/{1}", this.Numerator, this.Denominator);
        }
    }
    class MixedFranction : Exression
    {
        public MixedFranction(string val) { this.Value = val; }

        public override string Format()
        {
            throw new NotImplementedException();
        }
    }

    interface IExtractor
    {
        IList<Exression> Extract(string expression);
    }
    class Extractor : IExtractor
    {
        System.Text.RegularExpressions.Regex rgExp = new System.Text.RegularExpressions.Regex(@"[+-/*_\d]+");
        HashSet<string> ops = new HashSet<string>(new string[] { "+", "-", "*", "/" });
        public IList<Exression> Extract(string expression)
        {
            IList<Exression> expressions = new List<Exression>();
            foreach (System.Text.RegularExpressions.Match m in rgExp.Matches(expression))
            {
                if (ops.Contains(m.Value)) // "+", "-", "*", "/"
                    expressions.Add(new Operator(m.Value));
                else if (m.Value.IndexOf("_") > -1 && m.Value.IndexOf("/") > -1) // 1_3/4
                    expressions.Add(new MixedFranction(m.Value));
                else if (m.Value.IndexOf("/") > -1) // 3/4
                    expressions.Add(new Franction(m.Value));
                else
                    expressions.Add(new Whole(m.Value));
            }

            return expressions;
        }
    }

}
