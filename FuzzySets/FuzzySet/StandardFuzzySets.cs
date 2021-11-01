using FuzzySets.Operations;

namespace FuzzySets.FuzzySet
{
    public class StandardFuzzySets
    {
        public class L_FUNCTION : IIntUnaryFunction
        {
            private readonly int _alpha;
            private readonly int _beta;

            public L_FUNCTION(int alpha, int beta)
            {
                _alpha = alpha;
                _beta = beta;
            }

            public double ValueAt(int x)
            {
                if (x < _alpha)
                {
                    return x;
                }
                else if (x < _beta)
                {
                    return (double)(_beta - x) / (_beta - _alpha);
                }
                return 0;
            }
        }

        public class GAMMA_FUNCTION : IIntUnaryFunction
        {
            private readonly int _alpha;
            private readonly int _beta;

            public GAMMA_FUNCTION(int alpha, int beta)
            {
                _alpha = alpha;
                _beta = beta;
            }

            public double ValueAt(int x)
            {
                if (x < _alpha)
                {
                    return 0;
                }

               
                if (x < _beta)
                {
                    return (double)(x - _alpha) / (_beta - _alpha);
                }
                
                return 1;
            }
        }

        public class LAMBDA_FUNCTION : IIntUnaryFunction
        {
            private readonly int _gamma;
            private readonly int _alpha;
            private readonly int _beta;

            public LAMBDA_FUNCTION(int alpha, int beta, int gamma)
            {
                _alpha = alpha;
                _beta = beta;
                _gamma = gamma;
            }
            public double ValueAt(int x)
            {
                if (x < _alpha)
                {
                    return 0;
                }

                if (x < _beta)
                {
                    return (double)(x - _alpha) / (_beta - _alpha);
                }

                if (x < _gamma)
                {
                    return (double)(_gamma - x) / (_gamma - _beta);
                }

                return 0;
            }
        }

        public static IIntUnaryFunction LFunction(int alpha, int beta)
        {
            return new L_FUNCTION(alpha, beta);
        }

        public static IIntUnaryFunction LambdaFunction(int alpha, int beta, int gamma)
        {
            return new LAMBDA_FUNCTION(alpha, beta, gamma);
        }

        public static IIntUnaryFunction GammaFunction(int alpha, int beta)
        {
            return new GAMMA_FUNCTION(alpha, beta);
        }

        
    }
}