# Generate-maze-using-Prim-s-algorithm

The game starts by having the player spawned on the terrain at position (40, 1, 15) and there are three floating objects above, 
where one of them is a green cube, the other is a red cylinder and the last is a yellow sphere. When running the game, 
there will be a forest of trees in a corner of the terrain.


If the player shoots these objects, then they get destroyed. If the player shoots any other stuff, they are not effected by the projectile. 
The player gets to shoot a projectile after every 1.2 seconds, even if the projectile got destroyed before this amount of time to wait, 
as shown in the PlayerController script, the script component of the player. The projectile exists for 1.0 seconds but I made the player 
shoot after 1.2 seconds to avoid having a chance of having more than one projectile exist. I instantiate the projectile though inside 
the PlayerController script, because it was written in the specifications that the projectile should come out of the player horizontally.


I detected if the projectile collides with the objects in the CubeController script, which is a script component of the green cube,
the SphereController script, which is a script component of the yellow sphere, and the CylinderController script, 
which is a script component of the red cylinder. I made it such that they will get destroyed if hit by the projectile. 


I detected if the projectile collides with any other stuff (i.e. the bushes, the trees, the walls and the ground) in the scene in the ProjectileController script, which 
is a script component of the projectile. I made it such that they won't get destroyed if hit by the projectile, and that only the projectiles get destroyed.


I made my projectile an orange cube of dimensions 0.2 x 0.2 x 0.2 since my player is of size 0.3 x 1.0 x 0.3. The reason why I used a 
cube shape to make the projectile instead of a capsule, or a cylinder shape was because it is easier to align it's orientation with the direction that the camera 
is facing. 


I used 3 directional lights in the scene to avoid darkness in certain important places if that's okay. 


I put the three objects as children of an empty gameobject called Objects as shown in the transformation hierarchy pane, which has 
a script component called ObjectsConroller. This script just stores a public static int variable called countRemainingObjects, 
which is initialized to 3. If a projectile hits any of the three floating objects, their script controller 
will first decrement from countRemainingObjects before destroying the gameObject. For instance, if a projectile hits the yellow sphere, 
the SphereController script will decrement from countRemainingObjects before destroying the sphere. The same goes for the other
two floating objects. 


The way I created the bushes is by having three spheres attached to each other, colored in bushy-green color.


The key consists of a cylinder, two cubes and a sphere. The key is colored in gold. There is a public static boolean variable 
in the GameController script called playerHasKey that the PlayerController uses to notify the GameController script that the player has 
collided with the key and made it disappear so that the GameController can then randomly generate the entrance tile, the exit tile, 
the path of the maze and then generate the maze for when time = 0 to time = 16. 


There is a dictionary called trees in the GameController script that stores as key the position of each tree and the tree gameobject at that position as value.


There is also another dictionary called activeTreesAtTimes that stores as key-value pairs the timestep throughout the maze as the key and as value, it takes in a dictionary
that has as key-value pair a vector3 which refers to the position of a tree as the key and a boolean as value, and each boolean that 
is being referred to by the tree position in that dictionary is used to specify whether the tree at that position is active or not.
Once scanned through all trees in the trees dictionary, the <Vector3, bool> dictionary is then added into the activeTreesAtTimes 
dictionary with the time it was called at to record the trees active or not at the current timestep. 


Each tile is of size 2x2. There is a 2d tiles array that stores all the tiles in the maze. 


At each timestep, the maze changes and if it changed once at a timestep, it gets recorded in the activeTreesOrNot dictionary in order for it to regenerate at that
timestep once reset or the player reached the maximum number of steps to proceed from the start to the exit. Otherwise, Prim's algorithm is called to randomly generate the maze for the current 
timestep and then records it inside the activeTreesOrNot dictionary. 


I made the maze change such that when the player goes from one tile to another, it does not get pushed away by the trees thus not skipping 
any timesteps when the player moves from one tile to another. Also, I made the corner trees wider to avoid diagonal movements. 


The tile color that indicates the tile on the randomly generated path at the next timestep for the player to go from the start to the exit tile is colored in red and the 
tile color that indicates the exit (goal) tile that the player must reach to win the game is colored in yellow. Once the player
reached the exit tile, the player object is destroyed and the "You win!" text is then displayed on the screen.

If the red color lands on the exit tile, in which case that the red color mixes with the yellow color, I made the color of 
the exit tile become reddish-yellow (orange-like) color as a result.


I do not let the player pitch along with the camera because if it does, it may go below the y-position of the terrain and it can cause
the player to fall from the terrain and into the world. This is why I let the projectile be shot in the direction of the camera and
start in the camera's position. 


Whenever clicking esc button or if the player moved 16 steps and still did not win the game, the player is teleported outside of the maze 
but close to it.


Whenever I open my unity editor, I keep on getting the error 
"Unable to parse file ProjectSettings/ProjectVersion.txt: [mapping values are not allowed in this context] at line 1." 
If you get it when grading my assignment, please ignore it or clear it out from the console and just run the game. Also
I used unity version 2019.2.4f1 and Microsoft visual studios 2019 to do my projet as mentioned in the specifications.


I did not bother checking the condition of whether the player is blocked on a tile inside the maze or not because I think
there will be no chance that it will happen since each tile in the maze is reachable by Prim's algorithm. And thus, writing a code for that
would be useless.


The only pre-asset that I used in unity is the TerrainTextures package which contains GrassUV01.png and it's meta file as well.
I used it in order to make a grass texture on the terrain since it would take me some time to figure out how to make the grass on the 
terrain myself.


The way I tracked the path is in the FindPath() method in GameController.cs, which uses the global path list declared in that script.
At the begininig of the do-while loop which executes at least once and checks if the total number of tiles contained inside the path
is at most 16 tiles (meaning the path from entrance to exit is not more than 16 steps), I clear the whole path list, 
then the enterance tile called enter is inserted into the path list, and I make the current tile be the last element currently 
inserted into the path. Then in the while loop inside which keeps on executing until the current tile is the exit, 
I chose the random neighbor of the current tile, then put that random neighbor inside the path list, then I select the random neighbor 
of that neighbor, and so on until the current neighboring tile is the exit.


The name of the scene is SampleScene if that's okay. I hope it's okay if I did not give it a different name.


If the player wins the game, a grey text "You Win!" appears on the screen and player gets destroyed.


The challenges I found in this assignment was recording the trees active or not at each timestep but I had a lot of fun doing this 
assignment anyhow and I hope it works perfectly well.




Not all the files were uploaded here because the size here would exceed 100 mb
