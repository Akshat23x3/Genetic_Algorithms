# Genetic_Algorithms
AI,Gamedev enthusiast #Akshat Khare

So this here what you are about to see is Genetic Algorithm - One Max Tests

What are Genetic Algorithms : 
Genetic Algorithms are basically a part of Evolutionary Computing, where you see an algorithm evolving through it's own experience through many generations.
Genetic Algorithms Basically works on the principles of Reproductive Biology, but why take inspiration from biology what does programming has to do with biology.
Actually many Machine Learning Algorithms are based on or inspired from the biological enviorment around us for example the infamous Reinforcement Learning is an absolute example 
of evolution learning. We will spent time on Reinforcement Learning but later. Here I am basically explaining the ideas and uses of genetic algorithms. 

The Catch in Reproductive Biology : 
Many of us know the biological reprodution process but if you have forgotten about it here is the short term memory recap.

Reproduction in Living Biengs are classified basically in two forms:
1.Sexual Reproduction
2.Asexual Reproduction

Sexual Reproduction : 
In easy words sexual reproduction requires two parents. These two parents' contains chromosomes which contains genes or (DNA). Each gene composes of the information of the respective
individual. Now what is this information , basically information means what conditions the individual faced and which actions it took to solve the conditions, now the actions taken by the 
individual may not be precise or optimum to solve the condition, but it carries a crucial information for the respective condition. Back to reproduction, these genes are crossed over with there 
mates. Now there are many ways to crossover the genes but here in this test I chose the one point cross-over method where one half genes would be equipped with one parent genes and another half by another parent genes.
But our work here doesn't end beacause may be your gene be similar to your parent but is not a complete reflection, to maintain the variance in the chidren we mutate the crossed-over chromosome genes.
Which will make them a different personality. Again there are multiple ways of mutating the child chromosome but here we will use flip-bit mutation where a single action from gene is flipped with a randomly selected action.
After mutating we have completely different indivduals which carry genes of there parents or previous generation.

But how does it get better.
Actually before crossing-over the genes, we select the best performing parents we could from the generation and store them into a pool of best chromosomes. Then we cross-over these pool of chromsomes with each other till a certain population.
Now these crossed over chromosomes are derived from the best chromsomes in the previous generation. But we need to diffrentiate the new chromsomes with each other. So the mutation happens with these chromsomes and the child population
is ready to be tested and passed over to the next generation.

But wait how does the selection happens.
The selection happens on the basis of performance, but how do we measure the performance. We make a method called fitness which is mostly an integer value. Every test has there own fitness manipulation but in One max tests we happen to calculate
the sum of the information present in the genes(which is an array of ones and zeros of a certain length).

We perform the generations till the optimal solution is not found.

One Max Tests : 
One max tests have a chromosome of with genes - an array or list of ones and zeros , fitness(sum of the list).
For example:
chromosome[0].genes = [1 , 1 , 0, 1, 0]
chromosome[0].fitness = 3

The generations will be performed till an optimal solution is not reached :
Here, optimal solution  : [1 ,1, 1, 1 , 1]

Asexual Reproduction : 

Every thing is simular to sexual reproduction but here, we only have one parent, no cross-over, no selection, no population, only mutation.
The Generations are performed where the weak off-spring is ignored and only that chromosome is selected which performed better than it's parent.

Thank You
