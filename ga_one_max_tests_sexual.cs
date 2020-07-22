using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_proj
{
    
    //The Chromosome Class
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

    class Solving //Solving Class
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
            //Running the tests or generations
            solve();
            Console.WriteLine("Achieved!!!");

        }
        
        //Fitness Calculation
        public int get_fitness(List<int> genes)
        {
            return genes.Sum(); // Sum of genes eg : [1, 1, 0, 1, 0] : fitness = 1 + 1+ 0+ 1 = 3
        }
        
        //Generating Parent
        public Chromosome generate_parent()
        {
            List<int> parent_genes = new List<int>();
            int fitness;
            for(int i = 0; i < length; i++)
            {
                parent_genes.Add(rand.Next(0, 2));
            }
            fitness = get_fitness(parent_genes);
            
            //Creating the object parent
            return new Chromosome(parent_genes, fitness);
        }
        
        //Initializing the parent population
        public List<Chromosome> create_population()
        {
            List<Chromosome> population_array = new List<Chromosome>();
            for(int i = 0; i < population; i++)
            {
                population_array.Add(generate_parent());
            }

            return population_array;
        }
        
        //Selection the best parent_chromosomes
        public List<Chromosome> selection(List<Chromosome> population_list)
        {
            
            int selected_length = Convert.ToInt32(0.2 * population_list.Count); //Intiallizing the length of the pool of the best parent (TOP 20% OF THE POPULATION)
            int[] fitness = new int[population_list.Count]; int max_fitness; int index;
            List<Chromosome> selected_population_list = new List<Chromosome>();

            for(int i = 0;i < population_list.Count;i++)
            {
                fitness[i] = population_list[i].fitness; // Creating the fitness list on which seletion is dependent
            }

            for(int i = 0;i < population_list.Count;i++)
            {
                max_fitness = fitness.Max();
                index = Array.IndexOf(fitness, max_fitness);
                selected_population_list.Add(population_list[index]);
                fitness[index] = -10;
                
            }

            selected_population_list.RemoveRange(selected_length, population_list.Count - selected_length); // sorting the top 20% of the population

            return selected_population_list;
        }

        public List<Chromosome> crossover(List<Chromosome> population_list) // Crossing-over the best chromosomes
        {
            List<Chromosome> selected_population_array = selection(population_list);
            List<Chromosome> crossed_chromosomes = new List<Chromosome>();
            int[] child_chromosome_genes = new int[length];
            int random_index_1 = new int(); int random_index_2 = new int();

            while (crossed_chromosomes.Count < population) //Crossing-over the best chromosomes until a certain population is reached
            {
                random_index_1 = rand.Next(selected_population_array.Count);
                random_index_2 = rand.Next(selected_population_array.Count);
                for(int i = 0;i < Convert.ToInt32(length / 2);i++ )
                {
                    child_chromosome_genes[i] = selected_population_array[random_index_1].genes[i]; // First half to be filled with firt parent
                    child_chromosome_genes[i + Convert.ToInt32(length / 2) - 1] = selected_population_array[random_index_2].genes[i + Convert.ToInt32(length / 2) - 1];// Second half to be filled with Second parent
                }
               
                // Adding crossed-over chromosome to the new population list
                crossed_chromosomes.Add(new Chromosome(child_chromosome_genes.ToList<int>(), get_fitness(child_chromosome_genes.ToList<int>())));
            }

            return crossed_chromosomes;
        }

        public List<Chromosome> mutation(List<Chromosome> population_list)
        {
            List<Chromosome> crossed_population_list = crossover(population_list);
            int index = new int();int random_gene = new int();

            for(int i = 0;i < population;i++) // Flip-Bit mutation where random gene is selected and flipped over with random geneset
            {
                index = rand.Next(length);
                random_gene = rand.Next(2);
                if (crossed_population_list[i].genes[index] == 0)
                    crossed_population_list[i].genes[index] = 1;
                else
                    crossed_population_list[i].genes[index] = 0;
                crossed_population_list[i].fitness = get_fitness(crossed_population_list[i].genes);
            }

            return crossed_population_list; // Finally the final population is ready

        }

        public int display(List<Chromosome> population_list, int gen) // Displaying the generation
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

        public void won(List<Chromosome> population_list) // If optimal solution if found display the solution and the genes of the Chromosome
        {
            Console.WriteLine("Best_Chromosome : " + String.Join(" ",selection(population_list)[0].genes));
        }

        public void solve() //Finally solving the tests
        {
            int gen = 0;
            List<Chromosome> parent_population_list = create_population(); // Intiallized the parent population
            List<Chromosome> child_population_list = new List<Chromosome>(); // Intiallized the child population
            int max_fitness = display(parent_population_list, gen); //Max fitness or the fitness value of best performing chromosome 

            if (max_fitness >= length)
            {
                won(parent_population_list);
                return;
            }

            while (true)
            {
                gen += 1;
                child_population_list = mutation(parent_population_list);//Child population mutated and performed
                max_fitness = display(child_population_list, gen); //Max fitness in the child population
                if (max_fitness >= length)
                {
                    won(child_population_list); //if chromosome from the child population cracks the optimal solution the tests stop and display the best chromosome
                    break;
                }
                parent_population_list = child_population_list; // If optimal solution is the not found the Chromosomes are passed to next generation and 
                                                                // process repeats until the optimal solution        
            }
        }
        
    }


    class Program
    {


        static void Main(string[] args)
        {
            Console.Clear();
            Solving solv = new Solving(); // Calling the class
            Console.ReadLine();
        }
    }
}
