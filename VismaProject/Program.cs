// See https://aka.ms/new-console-template for more information
using System;
using System.IO;

namespace VismaProject{
internal class NewBaseType
{
    private static void Main(string[] args)
    {

        Console.Clear();
        Console.WriteLine("Welcome to the Visma's resource managing shortage! please enter your username here:");

        User user = new User();         //send username to the class
        user.menu();



    }
}
}


