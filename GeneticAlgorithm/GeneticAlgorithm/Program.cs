using System;
using GeneticAlgorithm;

double Function(double[] inputs)
{
    var x = inputs[0];
    var y = inputs[1];
    var beta0 = inputs[2];
    var beta1 = inputs[3];
    var beta2 = inputs[4];
    var beta3 = inputs[5];
    var beta4 = inputs[6];

    return (Math.Sin(beta0 + beta1 * x)) 
           + (beta2 * Math.Cos(x * (beta3 + y))) 
           * (1 / (1 + Math.Exp(Math.Pow((x - beta4), 2.0))));
}

var functionData = new FunctionData("Data/zad4-dataset1.txt", 2);
var genGet = new GenerativeGenAlgorithm(5, Function, functionData);
genGet.Run(100000);