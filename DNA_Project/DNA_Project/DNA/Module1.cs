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
            //TODO Parse string content to chunks object
            List<Chunck> chuncks = ParseRawDatas(content);


            int resPaire = await totalPaire(chuncks);
            CountBases resOccur = await totalOccurence(chuncks);

            return resOccur;
        }

        private List<Chunck> ParseRawDatas(String content)
        {
            List<Chunck> chuncks = new List<Chunck>();
            String[] lines = content.Split(new Char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var line in lines)
            {

                if(!line.Contains("#"))
                {
                    Chunck chunck = new Chunck(line);
                    chuncks.Add(chunck);
                }
            }
            return chuncks;
        }

        public async Task<int> totalPaire(List<Chunck> chuncks)
        {
            Console.WriteLine("Je suis module 1 : totalPaire");
            return chuncks.Count();
        }

        public async Task<CountBases> totalOccurence(List<Chunck> chuncks)
        {
            CountBases countBases = new CountBases();
            Console.WriteLine("Je suis module 1 : totalOccurence");
            Parallel.ForEach(chuncks, (chunck) =>
            {
                if (chunck.genotype.Length > 0)
                {
                    switch (chunck.genotype[0])
                    {
                        case 'A':
                            lock(countBases)
                            {
                                countBases.basesA++;
                            }
                            break;
                        case 'T':
                            lock (countBases)
                            {
                                countBases.basesT++;
                            }
                            break;
                        case 'G':
                            lock (countBases)
                            {
                                countBases.basesG++;
                            }
                            break;
                        case 'C':
                            lock (countBases)
                            {
                                countBases.basesC++;
                            }
                            break;
                        default:
                            lock (countBases)
                            {
                                countBases.basesUndefine++;
                            }
                            break;
                    }
                }
                if (chunck.genotype.Length > 1)
                {
                    switch (chunck.genotype[1])
                    {
                        case 'A':
                            lock (countBases)
                            {
                                countBases.basesA++;
                            }
                            break;
                        case 'T':
                            lock (countBases)
                            {
                                countBases.basesT++;
                            }
                            break;
                        case 'G':
                            lock (countBases)
                            {
                                countBases.basesG++;
                            }
                            break;
                        case 'C':
                            lock (countBases)
                            {
                                countBases.basesC++;
                            }
                            break;
                        default:
                            lock (countBases)
                            {
                                countBases.basesUndefine++;
                            }
                            break;
                    }
                }
            });
            return countBases;
        }

    }
}