using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_proj
{

    class Chromosome
    {
        public List<int> genes;
        public int fitness;

        public Chromosome(List<int> gene, int fit)
        {
            genes = gene;
            fitness = fit;
        }
    }

    class Solving
    {
        private int population;
        private int length;
        private Random rand = new Random();

        public Solving()
        {
            Console.Write("Enter the value of population : ");
            population = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nEnter the value of Length of chromosome : ");
            length = Convert.ToInt32(Console.ReadLine());
            solve();
            Console.WriteLine("Achieved!!!");

        }
        
        public int get_fitness(List<int> genes)
        {
            return genes.Sum();
        }

        public Chromosome generate_parent()
        {
            List<int> parent_genes = new List<int>();
            int fitness;
            for(int i = 0; i < length; i++)
            {
                parent_genes.Add(rand.Next(0, 2));
            }
            fitness = get_fitness(parent_genes);

            return new Chromosome(parent_genes, fitness);
        }

        public List<Chromosome> create_population()
        {
            List<Chromosome> population_array = new List<Chromosome>();
            for(int i = 0; i < population; i++)
            {
                population_array.Add(generate_parent());
            }

            return population_array;
        }

        public List<Chromosome> selection(List<Chromosome> population_list)
        {
            int selected_length = Convert.ToInt32(0.2 * population_list.Count);
            int[] fitness = new int[population_list.Count]; int max_fitness; int index;
            List<Chromosome> selected_population_list = new List<Chromosome>();

            for(int i = 0;i < population_list.Count;i++)
            {
                fitness[i] = population_list[i].fitness;
            }

            for(int i = 0;i < population_list.Count;i++)
            {
                max_fitness = fitness.Max();
                index = Array.IndexOf(fitness, max_fitness);
                selected_population_list.Add(population_list[index]);
                fitness[index] = -10;
                
            }

            selected_population_list.RemoveRange(selected_length, population_list.Count - selected_length);

            return selected_population_list;
        }

        public List<Chromosome> crossover(List<Chromosome> population_list)
        {
            List<Chromosome> selected_population_array = selection(population_list);
            List<Chromosome> crossed_chromosomes = new List<Chromosome>();
            int[] child_chromosome_genes = new int[length];
            int random_index_1 = new int(); int random_index_2 = new int();

            while (crossed_chromosomes.Count < population)
            {
                random_index_1 = rand.Next(selected_population_array.Count);
                random_index_2 = rand.Next(selected_population_array.Count);
                for(int i = 0;i < Convert.ToInt32(length / 2);i++ )
                {
                    child_chromosome_genes[i] = selected_population_array[random_index_1].genes[i];
                    child_chromosome_genes[i + Convert.ToInt32(length / 2) - 1] = selected_population_array[random_index_2].genes[i + Convert.ToInt32(length / 2) - 1];
                }
               
                crossed_chromosomes.Add(new Chromosome(child_chromosome_genes.ToList<int>(), get_fitness(child_chromosome_genes.ToList<int>())));
            }

            return crossed_chromosomes;
        }

        public List<Chromosome> mutation(List<Chromosome> population_list)
        {
            List<Chromosome> crossed_population_list = crossover(population_list);
            int index = new int();int random_gene = new int();

            for(int i = 0;i < population;i++)
            {
                index = rand.Next(length);
                random_gene = rand.Next(2);
                if (crossed_population_list[i].genes[index] == 0)
                    crossed_population_list[i].genes[index] = 1;
                else
                    crossed_population_list[i].genes[index] = 0;
                crossed_population_list[i].fitness = get_fitness(crossed_population_list[i].genes);
            }

            return crossed_population_list;

        }

        public int display(List<Chromosome> population_list, int gen)
        {
            int[] fitness = new int[population];
            for(int i = 0;i < population;i++)
            {
                fitness[i] = population_list[i].fitness;
            }
            int max_fitness = fitness.Max();
            Console.WriteLine("Gen : " + gen + " Max Fitness : " + max_fitness);

            return max_fitness;
        }

        public void won(List<Chromosome> population_list)
        {
            Console.WriteLine("Best_Chromosome : " + String.Join(" ",selection(population_list)[0].genes));
        }

        public void solve()
        {
            int gen = 0;
            List<Chromosome> parent_population_list = create_population();
            List<Chromosome> child_population_list = new List<Chromosome>();
            int max_fitness = display(parent_population_list, gen);

            if (max_fitness >= length)
            {
                won(parent_population_list);
                return;
            }

            while (true)
            {
                gen += 1;
                child_population_list = mutation(parent_population_list);
                max_fitness = display(child_population_list, gen);
                if (max_fitness >= length)
                {
                    won(child_population_list);
                    break;
                }
                parent_population_list = child_population_list;                
            }
        }
        
    }


    class Program
    {


        static void Main(string[] args)
        {
            Console.Clear();
            Solving solv = new Solving();
            Console.ReadLine();
        }
    }
}
