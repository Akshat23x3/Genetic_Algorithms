import random
import datetime
import numpy as np
import unittest

class Chromosome:

	def __init__(self, genes, fitness):
		self.genes = genes
		self.fitness = fitness


#OneMaxProblem-----------------------------------------------------------

class geneticonemax:

	def __init__(self):
		length = int(input('Enter the length of the chromosome : '))
		best_chromosome = self.get_best_chromosome(length)
		print('Achieved!!!', 'at fitness', best_chromosome.fitness)

	def generate_parent(self, length):
		self.parent_genes = [random.randint(0, 1) for _ in range(length)]
		self.parent_fitness = self.get_fitness(self.parent_genes)

		return Chromosome(self.parent_genes, self.parent_fitness)

	def get_fitness(self, chromosome_genes):
		return sum(chromosome_genes)

	def mutate(self, parent):
		child_genes = parent.genes[:]
		index = random.randint(0, len(parent.genes) - 1)
		child_genes[index] = 1 if child_genes[index] == 0 else 0
		child_fitness = self.get_fitness(child_genes)

		return Chromosome(child_genes, child_fitness)

	def get_best_chromosome(self, target_fitness):
		#random.seed()
		step = 0
		parent_chromosome = self.generate_parent(target_fitness)
		starttime = datetime.datetime.now()
		self.display(parent_chromosome, starttime,step)
		if parent_chromosome.fitness >= target_fitness:
			return parent_chromosome
		
		while True:
			step += 1
			child_chromosome = self.mutate(parent_chromosome)

			if parent_chromosome.fitness >= child_chromosome.fitness:
				continue
			self.display(child_chromosome, starttime,step)
			if child_chromosome.fitness >= target_fitness:
				return child_chromosome

			parent_chromosome = child_chromosome

	def display(self, chromosome, starttime, step):
		timediff = datetime.datetime.now() - starttime
		print(chromosome.genes[:15],'...',chromosome.genes[-15:],'  ',chromosome.fitness,'  ',timediff, step)


genetic = geneticonemax()
