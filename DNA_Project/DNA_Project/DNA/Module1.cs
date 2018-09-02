using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNA_Project.DNA
{
    internal class Module1 : Node
    {
        public Module1()
        {
        }

        public override async Task<Object> Process(String content)
        {
            var paireTask = totalPaire(content);
            var occurTask = totalOccurence(content);
            //TODO a completer

            await Task.WhenAll(paireTask, occurTask);

            int resPaire = await paireTask;
            int resOccur = await occurTask;

            return new int[resPaire, resOccur];
        }

        public async Task<int> totalPaire(String content)
        {
            Console.WriteLine("Je suis module 1 ");
            return 0;
        }

        public async Task<int> totalOccurence(String content)
        {
            return 0;
        }
    }
}