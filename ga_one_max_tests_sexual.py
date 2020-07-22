class OnemaxPopulation:
	mutation_probability = 1.0

	def __init__(self):
		self.length = int(input(' Enter the length of the array : '))
		self.population = int(input(' Enter the number of generations : '))
		self.solve()
		print('Achieved !!!')


	def create_population(self):
		population_array = []
		for i in range(self.population):
			population_array.append(self.generate_parent())

		return population_array

	def fitness(self, genes):
		return sum(genes)

	def generate_parent(self):
		parent_genes = [random.randint(0, 1) for _ in range(self.length)]
		parent_fitness = self.fitness(parent_genes)

		return Chromosome(parent_genes, parent_fitness)

	def selection(self, population_array):
		chromosomes = population_array.copy()
		selected_fitness = [i.fitness for i in chromosomes]
		selected_chromosomes = []

		for _ in range(self.length):
			max_fitness = max(selected_fitness)
			index = selected_fitness.index(max_fitness)
			selected_chromosomes.append(chromosomes[index])
			selected_fitness[index] = -99


		selected_chromosomes = selected_chromosomes[ : int(0.2 * self.population) + 1]

		return selected_chromosomes

	def crossover(self, population_array):
		chromosomes = self.selection(population_array)
		crossed_chromosomes = []
		i = 0
		index = random.randint(0, self.length - 4)

		#Elite sharing with cutoff genes
		while len(crossed_chromosomes) < self.population:
			child_chromosome_genes = []
			random_idx_1 , random_idx_2 = random.sample(range(len(chromosomes)), 2)
			child_chromosome_genes += chromosomes[random_idx_1].genes[ : int(self.length / 2)]
			child_chromosome_genes += chromosomes[random_idx_2].genes[int(self.length / 2) : ]
			child_fitness = self.fitness(child_chromosome_genes)
			crossed_chromosomes.append(Chromosome(child_chromosome_genes, child_fitness))


		return crossed_chromosomes

	def mutation(self, population_array):

		crossed_chromosomes = self.crossover(population_array)
		mutated_population = []
		mutated_fitness = None;mutated_genes = None

		index = [random.randint(0,self.length - 1) for _ in range(self.population)]

		for i, ind in zip(crossed_chromosomes, index):
			i.genes[ind] = 0 if i.genes[ind] == 1 else 1

		for chromosome in crossed_chromosomes:
			mutated_fitness = self.fitness(chromosome.genes)
			mutated_genes = chromosome.genes
			mutated_population.append(Chromosome(mutated_genes, mutated_fitness))

		return mutated_population

	def display(self, population_array, starttime, gen, total_fitness):
		timediff = datetime.datetime.now() - starttime
		print(timediff,'  Gen : ',gen,' Total_fit : ',total_fitness)

	def solve(self):
		starttime = datetime.datetime.now()
		population_array = self.create_population()
		gen = 0
		population_fitness = sum(i.fitness for i in population_array)
		self.display(population_array, starttime, gen, population_fitness)

		sum_fitness = 0
		sum_fitness = sum(i.fitness for i in population_array)

		while True:
			gen += 1
			self.mutation_probability = self.mutation_probability * 0.99
			child_population_array = self.mutation(population_array)
			child_fitness = sum(i.fitness for i in child_population_array)
			
			array = self.selection(child_population_array)
			if array[0].fitness >= self.length:
				print(array[0].genes,'  ',end = "")
				self.display(child_population_array, starttime, gen, child_fitness)
				break

			self.display(child_population_array, starttime, gen, child_fitness)
			population_array = child_population_array
			population_fitness = child_fitness

onemax = OnemaxPopulation()
