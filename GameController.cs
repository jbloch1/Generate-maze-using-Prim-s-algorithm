using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is used in the GameController class to create and initialize tiles with their positions
public class Tile
{
    Vector3 position;   //This is the position of the tile

    List<Tile> neighbors;   //This is the list of the neighboring tiles (left, right, above (on z-axis) and below (on z-axis))

    bool playerOnTile;  //Used to indicate that the player is on this tile

    public Tile(Vector3 pos)    //Tile constructor
    {
        this.position = pos;
        this.neighbors = new List<Tile>();
    }

    //GameController class sets playerOnTile to true if the player is on the tile or false if not
    public void SetPlayerOnTile(bool playerOnTile)
    {
        this.playerOnTile = playerOnTile;
    }


    public Vector3 GetPosition()
    {
        return position;
    }

    public void AddNeighborToList(Tile t)
    {
        neighbors.Add(t);
    }

    public List<Tile> GetNeighbors()
    {
        return neighbors;
    }


    public bool PlayerIsOnTile()
    {
        return playerOnTile;
    }
}


public class GameController : MonoBehaviour
{
    public static bool playerHasKey;    //This is used for the PlayerController to notify the GameController that the player got the key.

    public GameObject tree; //Public variable that refers to the tree  

    public GameObject player;   //Public variable that refers to the player

    public GameObject tileLight;    //Public variable that refers to the color of the next tile in the path (red color)

    public GameObject goalTileLight;    //Public variable that refers to the color of the exit tile (yellow color).

    public GameObject mixedTileLight;   //Public variable that refers to the color of the exit tile (orange color) when the red color lands on the exit tile which is of color yellow.

    public GameObject key;  //Public variable that refers to the key

    public Text youWinText; //Tells the user that the player won the game

    List<Tile> path = new List<Tile>(); //Ordered sequence of tiles needed to track the random path

    Tile enter;  //Entrance cell into the maze

    Tile exit; //Exit cell out of the maze

    bool playerInMaze;

    int time;  //3rd dimension of the maze (timestep)

    bool hasWon;    //Boolean flag for determining whether the player won the game by reaching the exit tile or not

    //This Dictionary stores all the trees in the forest as values and their positions as the keys
    Dictionary<Vector3, GameObject> trees = new Dictionary<Vector3, GameObject>();

    Tile currentTile; //Used to keep track of the tile the user is currently on.

    //This dictionary is used to store as value a dictionary that keeps track of which trees are set active or not at each time step which are the keys
    //The Dictionary value is a bool which determines whether the tree at a position (the key) is active or not.
    Dictionary<int, Dictionary<Vector3, bool>> activeTreesAtTimes = new Dictionary<int, Dictionary<Vector3, bool>>();

    Tile[,] tiles = new Tile[8,8];  //Stores all the tiles in the forest


    // Start is called before the first frame update
    void Start()
    {
        enter = null;   //Initializing it to null.
        exit = null;    //Initializing it to null.
        playerInMaze = false;
        hasWon = false;
        time = 0;   //Initializing it to 0.
        currentTile = null; //This will initially be null.
        CreateAndInitializeTiles(); //Creates and initializes tiles.
        SetTileNeighbors(); //Sets the horizontal (left and right) and vertical (up and down) neighbors of each tile.
        MakeForest();   //Adds the trees to the terrain in a specified position to create the forest
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHasKey) //Checks if player has the key
        {
            if(enter == null && exit == null) //This statement will be executed only once after the player got the key because then enter and exit will no longer be null throughout the game.
            {
                GameObject selectedTree; //Used for referring to a tree to be dealt with

                //Selecting the entry tile

                //Enterance cell is on horizontal boundary when random number 0 is generated
                if(Random.Range(0,2) == 0)
                {
                    enter = tiles[0, Random.Range(0, 7)];  //Random horizontal boundary tile will be selected, just not the one adjacent to the wall
                    
                    //Select the tree at this position adjacent to the entrance tile 
                    selectedTree = trees[new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z + 0.75f)];
                    Destroy(selectedTree); //Destroys this tree.

                    //Removes the destroyed selected tree's key from the list 
                    trees.Remove(new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z + 0.75f));

                    //Select the tree at this position adjacent to the entrance tile 
                    selectedTree = trees[new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z+0.50f)];


                    Destroy(selectedTree); //Destroys this tree.

                    //Removes the destroyed selected tree's key from the list.
                    trees.Remove(new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z + 0.50f));

                    //Select the tree at this position adjacent to the entrance tile.
                    selectedTree = trees[new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z + 0.25f)];

                    Destroy(selectedTree);//Destroys this tree.

                    //Removes the destroyed selected tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z + 0.25f));

                    //Select the tree at this position adjacent to the entrance
                    selectedTree = trees[new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z)];

                    Destroy(selectedTree); //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z));

                    //Select the tree at this position adjacent to the entrance
                    selectedTree = trees[new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z - 0.25f)];

                    Destroy(selectedTree); //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z - 0.25f));

                    //Select the tree at this position adjacent to the entrance
                    selectedTree = trees[new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z - 0.50f)];

                    Destroy(selectedTree); //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z - 0.50f));

                    //Select the tree at this position adjacent to the entrance
                    selectedTree = trees[new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z - 0.75f)];

                    Destroy(selectedTree); //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x - 1.0f, 0.0f, enter.GetPosition().z - 0.75f));

                }
                else //Enterance cell is on Veritcal boundary when random number 0 is generated
                {
                    enter = tiles[Random.Range(0, 7), 0];  //Random Vertical boundary tile will be selected, just not the one adjacent to the wall
                     

                    //Select the tree at this position adjacent to the entrance 
                    selectedTree = trees[new Vector3(enter.GetPosition().x - 0.75f, 0.0f, enter.GetPosition().z - 1.0f)];

                    Destroy(selectedTree);  //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x - 0.75f, 0.0f, enter.GetPosition().z - 1.0f));

                    //Select the tree at this position adjacent to the entry
                    selectedTree = trees[new Vector3(enter.GetPosition().x - 0.50f, 0.0f, enter.GetPosition().z - 1.0f)];

                    Destroy(selectedTree);  //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x - 0.50f, 0.0f, enter.GetPosition().z - 1.0f));

                    //Select the tree at this position adjacent to the entry
                    selectedTree = trees[new Vector3(enter.GetPosition().x - 0.25f, 0.0f, enter.GetPosition().z - 1.0f)];

                    Destroy(selectedTree);  //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x - 0.25f, 0.0f, enter.GetPosition().z - 1.0f));

                    //Select the tree at this position adjacent to the entry
                    selectedTree = trees[new Vector3(enter.GetPosition().x, 0.0f, enter.GetPosition().z - 1.0f)];

                    Destroy(selectedTree);  //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x, 0.0f, enter.GetPosition().z - 1.0f));

                    //Select the tree at this position adjacent to the entry
                    selectedTree = trees[new Vector3(enter.GetPosition().x + 0.25f, 0.0f, enter.GetPosition().z - 1.0f)];

                    Destroy(selectedTree);  //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x + 0.25f, 0.0f, enter.GetPosition().z - 1.0f));

                    //Select the tree at this position adjacent to the entry
                    selectedTree = trees[new Vector3(enter.GetPosition().x + 0.50f, 0.0f, enter.GetPosition().z - 1.0f)];

                    Destroy(selectedTree);  //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x + 0.50f, 0.0f, enter.GetPosition().z - 1.0f));

                    //Select the tree at this position adjacent to the entry
                    selectedTree = trees[new Vector3(enter.GetPosition().x + 0.75f, 0.0f, enter.GetPosition().z - 1.0f)];

                    Destroy(selectedTree);  //Destroys this tree.

                    //Removes the selected destroyed tree's key from the list
                    trees.Remove(new Vector3(enter.GetPosition().x + 0.75f, 0.0f, enter.GetPosition().z - 1.0f));
                }

                Debug.Log("Entrance: " + enter.GetPosition());  //Used for displaying on console the entrance tile position


                //Selecting the exit tile

                //Exit tile is on 7th row when random number 0 is generated.
                if(Random.Range(0, 2) == 0)
                {
                    exit = tiles[7, Random.Range(0, 8)];
                }
                else //Exit tile is on 7th column when random number 1 is generated.
                {
                    exit = tiles[Random.Range(0, 8), 7];
                }

                //Sets the position of the light of the goal tile to that of the exit tile on the x and z axis
                goalTileLight.transform.position = new Vector3(exit.GetPosition().x, 0.01f, exit.GetPosition().z);
                goalTileLight.SetActive(true);  //Sets the gameobject active to true

                Debug.Log("Exit: " + exit.GetPosition());   //Used For displaying on console the exit tile position 


                FindPath(); //Generates the random path to get from the enterance tile to the exit tile.

                //This for loop is used for checking the positions of each tile on th epath
                foreach (Tile t in path)
                {
                    Debug.Log(t.GetPosition()); //Displays the position of each tile on console.
                }
                
                PrimsAlgorithm();   //Calls this method to generate maze
                RemoveConsecutivePathTilesTrees(); //Calls this method to remove trees that block the path from tile path[time] to tile path[time+1]
                RecordCurrentActiveTrees(); //Calls this method to record which trees are active and which aren't
            }

            
            //Double-nested for loop looping through each tile in tiles array, which are tiles of the forest
            for(int row = 0; row < tiles.GetLength(0); row++)
            {
                for(int column = 0; column < tiles.GetLength(1); column++)
                {
                    float tileMinPosX = tiles[row, column].GetPosition().x - 1.0f;  //Where tile begins on x-axis 
                    float tileMaxPosX = tiles[row, column].GetPosition().x + 1.0f;  //Where tile ends on x-axis

                    float tileMinPosZ = tiles[row, column].GetPosition().z - 1.0f;  //Where tile begins on z-axis
                    float tileMaxPosZ = tiles[row, column].GetPosition().z + 1.0f;  //Where tile ends on z-axis


                    if(player)  //This condition is to avoid all bunch of errors generated when the player is destroyed after the game ends. 
                    {
                        //Checking if player is on the current tile
                        if (player.transform.position.x >= tileMinPosX && player.transform.position.x <= tileMaxPosX && player.transform.position.z >= tileMinPosZ && player.transform.position.z <= tileMaxPosZ)
                        {
                            playerInMaze = true;
                        }
                        else playerInMaze = false;

                        //The 2 nested if statements execute if the player is on the current tile.
                        if (player.transform.position.x >= (tileMinPosX + 0.25f) && player.transform.position.x <= (tileMaxPosX - 0.25f))
                        {
                            if (player.transform.position.z >= (tileMinPosZ + 0.25f) && player.transform.position.z <= (tileMaxPosZ - 0.25f))
                            {
                                if (currentTile == null)
                                    currentTile = tiles[row, column];

                                //This condition checks if the player moved a tile
                                if (currentTile != tiles[row, column])
                                {
                                    currentTile = tiles[row, column];   //If so, then assign the tile the player is currently on to the currentTile variable
                                    time++; //Increment the time after player moves a tile

                                    Debug.Log("Time: " + time); //Displays the amount of time steps passed.

                                    //All trees are set active before setting some of these to non active for the next timestep.
                                    foreach (KeyValuePair<Vector3, GameObject> tree in trees)
                                    {
                                        if (!tree.Value.activeSelf)
                                        {
                                            tree.Value.SetActive(true);
                                        }
                                    }

                                    //Checks if the maze for the next time was already generated
                                    if (activeTreesAtTimes.ContainsKey(time))
                                    {
                                        GetMazeAtNextTimeStep();

                                        //Checks if the time variable is less than the last index into the path list so that there are no out of bounds exceptions.
                                        if (time < path.Count - 1)
                                            tileLight.transform.position = new Vector3(path[time + 1].GetPosition().x, 0.01f, path[time + 1].GetPosition().z);  //Sets the color of next tile in the path list                                           
                                    }
                                    else //Otherwise
                                    {
                                        PrimsAlgorithm();   //Generate maze for this timestep

                                        //Finding the distance between the entrance tile and it's neighbor in the selected path
                                        if (time < path.Count - 1)
                                        {
                                            RemoveConsecutivePathTilesTrees();  //Remove trees that block path from next tile in the path's list to it's neighboring tile in the path
                                        }

                                        RecordCurrentActiveTrees(); //Records the trees active or not at the next timestep
                                    }

                                    //If the tracking tile color (red color) is on the exit tile color (yellow color), they mix together to make an orange color on the exit tile.
                                    if (tileLight.transform.position == goalTileLight.transform.position)   
                                    {
                                        tileLight.SetActive(false); //Setting the red color active to false
                                        goalTileLight.SetActive(false); //Setting the yellow color to false
                                        mixedTileLight.transform.position = new Vector3(exit.GetPosition().x, 0.01f, exit.GetPosition().z); //Setting the orange color to be the position of the red and yellow color.
                                        mixedTileLight.SetActive(true); //Setting orange color to be active to true.
                                    }
                                }


                                //If the player reached the exit tile
                                if (currentTile == exit)
                                {
                                    youWinText.text = "You Win!";   //Player won the game
                                    Destroy(player);    //Player is destroyed
                                    hasWon = true;
                                }


                                //Checks if time = 16 and player still did not win the game
                                if (time == 16 && !hasWon)
                                {
                                    //If so, then reset time = 0 and respawn player at start position
                                    time = 0; 
                                    Respawn();
                                }
                            }
                        }

                        
                        //If the user presses escape, then reset time = 0 and respawn player at start position
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            if(playerInMaze)
                            {
                                time = 0;
                                Respawn();
                            }
                        }
                    }
                    
                }
            }
        }
    }

    void RemoveConsecutivePathTilesTrees()
    {
        //Finding the distance between the current tile and it's neighbor in the selected path
        Vector3 distance = path[time + 1].GetPosition() - path[time].GetPosition();

        //All trees that block the path between the two neighboring tiles on the path gets destroyed, depends where the neighbor is 

        //The neighboring tile on the path is to the right of the current tile, therefore, the blocking trees to the right of the current tile disappears
        if (distance.x == 2.0f)
        {
            trees[new Vector3(path[time].GetPosition().x + 1.0f, 0.0f, path[time].GetPosition().z + 0.75f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 1.0f, 0.0f, path[time].GetPosition().z + 0.50f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 1.0f, 0.0f, path[time].GetPosition().z + 0.25f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 1.0f, 0.0f, path[time].GetPosition().z)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 1.0f, 0.0f, path[time].GetPosition().z - 0.25f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 1.0f, 0.0f, path[time].GetPosition().z - 0.50f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 1.0f, 0.0f, path[time].GetPosition().z - 0.75f)].SetActive(false);
        }
        //The neighboring tile on the path is to the left of the current tile, therefore, the blocking trees to the left of the current tile disappears
        else if (distance.x == -2.0f)
        {
            trees[new Vector3(path[time].GetPosition().x - 1.0f, 0.0f, path[time].GetPosition().z + 0.75f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x - 1.0f, 0.0f, path[time].GetPosition().z + 0.50f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x - 1.0f, 0.0f, path[time].GetPosition().z + 0.25f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x - 1.0f, 0.0f, path[time].GetPosition().z)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x - 1.0f, 0.0f, path[time].GetPosition().z - 0.25f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x - 1.0f, 0.0f, path[time].GetPosition().z - 0.50f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x - 1.0f, 0.0f, path[time].GetPosition().z - 0.75f)].SetActive(false);
        }
        //The neighboring tile on the path is above the current tile, therefore, the blocking trees above the current tile disappears
        else if (distance.z == 2.0f)
        {
            trees[new Vector3(path[time].GetPosition().x - 0.75f, 0.0f, path[time].GetPosition().z + 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x - 0.50f, 0.0f, path[time].GetPosition().z + 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x - 0.25f, 0.0f, path[time].GetPosition().z + 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x, 0.0f, path[time].GetPosition().z + 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 0.25f, 0.0f, path[time].GetPosition().z + 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 0.50f, 0.0f, path[time].GetPosition().z + 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 0.75f, 0.0f, path[time].GetPosition().z + 1.0f)].SetActive(false);
        }
        //The neighboring tile on the path is below the current tile, therefore, the blocking trees to the below the current tile disappears
        else if (distance.z == -2.0f)
        {
            trees[new Vector3(path[time].GetPosition().x - 0.75f, 0.0f, path[time].GetPosition().z - 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x - 0.50f, 0.0f, path[time].GetPosition().z - 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x - 0.25f, 0.0f, path[time].GetPosition().z - 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x, 0.0f, path[time].GetPosition().z - 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 0.25f, 0.0f, path[time].GetPosition().z - 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 0.50f, 0.0f, path[time].GetPosition().z - 1.0f)].SetActive(false);
            trees[new Vector3(path[time].GetPosition().x + 0.75f, 0.0f, path[time].GetPosition().z - 1.0f)].SetActive(false);
        }

        //This sets the current position of the tile at each timestep
        tileLight.transform.position = new Vector3(path[time + 1].GetPosition().x, 0.01f, path[time + 1].GetPosition().z);

        //Sets tile active if it did not get activated yet(Which happens only at the beginning when time = 0)
        if(!tileLight.activeSelf)
            tileLight.SetActive(true);
    }

    //This method sets the trees at this timestep active and inactive as recorded
    void GetMazeAtNextTimeStep()
    {
        foreach (KeyValuePair<Vector3, bool> active in activeTreesAtTimes[time])
        {
            if (active.Value == false)
            {
                trees[active.Key].SetActive(false);
            }
        }
    }

    //This method is used for respawning the player at entrance position after setting time = 0 in the Update function
    void Respawn()
    {
        foreach (KeyValuePair<Vector3, bool> active in activeTreesAtTimes[time])
        {
            if(active.Value == false)
            {
                trees[active.Key].SetActive(false);
            }else trees[active.Key].SetActive(true);
        }

        
        //Teleports the player outside the maze and close to the entrance tile
        player.transform.position = new Vector3(enter.GetPosition().x-1.5f, player.transform.position.y, enter.GetPosition().z - 1.5f);
        currentTile = null;

        //Setting the position of the tileLight to be that of the next tile on the path which is at time = 1 in this case.
        tileLight.transform.position = new Vector3(path[time + 1].GetPosition().x, 0.01f, path[time + 1].GetPosition().z);
        tileLight.SetActive(true);  //Setting the path tracking tile light (red color) active to true.
        goalTileLight.SetActive(true);  //Setting the goal tile color (yellow color) active to true.
        mixedTileLight.SetActive(false);    //Setting the mixed tile color (orange color) active to false.
    }

    void FindPath()
    {
        Tile current = null;    //Used for referring to the last tile in the path's list 

        do
        {
            path.Clear();   //Clear list from whatever is inside it

            path.Add(enter);    //Adds the entrance tile to the path

            current = path[path.Count - 1]; //Makes current point first to the entrance tile

            while (current != exit) //Executes until the player did not reach the exit tile
            {
                List<Tile> tileNeighbors = current.GetNeighbors();  //Gets all the neighbors of the current tile.
                current = tileNeighbors[Random.Range(0, tileNeighbors.Count)];  //Selects one of them to be next on the path
                path.Add(current);  //Add that selected neighbor into the path list
            }
        } while (path.Count > 16); //Executes as long as there are more than 16 tiles on the path.
        
    }

    //This method is used for creating and initializing the positions of each tile in the forest.
    void CreateAndInitializeTiles()
    {
        for(int row = 0; row < tiles.GetLength(0); row++)
        {
            for(int column = 0; column < tiles.GetLength(1); column++)
            {
                tiles[row, column] = new Tile(new Vector3(35.0f + (2 * row), 0.0f, 45.0f + (2*column)));
            }
        }
    }

    /* This method calls findNeighbors(int row, int column) on each tile in the 
     * array to do the job of finding their horizontal and vertical neighbors*/
    void SetTileNeighbors()
    {
        for(int row = 0; row < tiles.GetLength(0); row++)
        {
            for(int column = 0; column < tiles.GetLength(1); column++)
            {
                findNeighbors(row, column);
            }
        }
    }

    //https://stackoverflow.com/questions/652106/finding-neighbours-in-a-two-dimensional-array/38296917#38296917
    //In this method, i refers to the row number of the current element and j refers to the column number of the current element
    void findNeighbors(int i, int j)
    {
        int rowLimit = tiles.GetLength(0) - 1;  //stores the last row element number in tiles array
        int columnLimit = tiles.GetLength(1) - 1;   //stores the last column element number in tiles array

        int firstRow = i - 1 >= 0 ? i - 1 : 0;  //Ternary operator that returns the biggest of i-1 or 0 
        int lastRow = i + 1 <= rowLimit ? i + 1 : rowLimit; //Ternary operator that returns the smallest of i+1 or the last row element

        int firstColumn = j - 1 >= 0 ? j - 1 : 0;   //Ternary operator that returns the biggest of j-1 or 0 
        int lastColumn = j + 1 <= columnLimit ? j + 1 : columnLimit;    //Ternary operator that returns the smallest of j+1 or the last column element

        //Looping through each tile in the tiles 2D array
        for (int row = firstRow; row <= lastRow; row++)
        {
            for (int column = firstColumn; column <= lastColumn; column++)
            {
                if ((row != i ^ column != j)) //The caret operator means xor, meaning that only one of the conditions have to be true
                {
                    tiles[i,j].AddNeighborToList(tiles[row,column]); //Adding this tile to the neighbors list of the current tile
                }
            }
        }
    }

    //This method is used for adding the trees at the positions specified in the instantiate() calls
    void MakeForest()
    {
        for(int row = 0; row < tiles.GetLength(0); row++)
        {
            for(int column = 0; column < tiles.GetLength(1); column++)
            {
                GameObject myTree;

                myTree = Instantiate(tree, new Vector3(tiles[row,column].GetPosition().x - 1.0f, 0.0f, tiles[row, column].GetPosition().z -1.0f), Quaternion.identity) as GameObject;
                myTree.transform.localScale = new Vector3(1.0f, 2.0f, 1.0f);    //This is to prevent diagonal movement of the player
                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x - 1.0f, 0.0f, tiles[row, column].GetPosition().z - 0.75f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x - 1.0f, 0.0f, tiles[row, column].GetPosition().z -0.50f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x - 1.0f, 0.0f, tiles[row, column].GetPosition().z - 0.25f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);


                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x - 1.0f, 0.0f, tiles[row, column].GetPosition().z), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);


                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x - 1.0f, 0.0f, tiles[row, column].GetPosition().z + 0.25f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x - 1.0f, 0.0f, tiles[row, column].GetPosition().z + 0.50f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x - 1.0f, 0.0f, tiles[row, column].GetPosition().z + 0.75f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x - 0.75f, 0.0f, tiles[row, column].GetPosition().z - 1.0f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x - 0.50f, 0.0f, tiles[row, column].GetPosition().z - 1.0f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x - 0.25f, 0.0f, tiles[row, column].GetPosition().z - 1.0f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x, 0.0f, tiles[row, column].GetPosition().z - 1.0f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x + 0.25f, 0.0f, tiles[row, column].GetPosition().z - 1.0f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x + 0.50f, 0.0f, tiles[row, column].GetPosition().z - 1.0f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);

                myTree = Instantiate(tree, new Vector3(tiles[row, column].GetPosition().x + 0.75f, 0.0f, tiles[row, column].GetPosition().z - 1.0f), Quaternion.identity) as GameObject;

                trees.Add(myTree.transform.position, myTree);
            }
        }
    }

    //The below link is the citation I found related to Prim's algorithm
    //http://weblog.jamisbuck.org/2011/1/10/maze-generation-prim-s-algorithm
    //This method executes prim's algorithm for generating the maze
    void PrimsAlgorithm()
    {
        List<Tile> frontierTiles = new List<Tile>();    //This list stores all frontier tiles
        List<Tile> mazeTiles = new List<Tile>();    //This list stores all tiles already in the maze

        mazeTiles.Add(enter);   //Adding the entrance tile into the maze.

        //Adds each neighbor into the list of frontier tiles.
        foreach (Tile neighbor in enter.GetNeighbors())  
        {
            frontierTiles.Add(neighbor);
        }

        //While there are still tiles not inside the maze
        while(frontierTiles.Count != 0)
        {
            int index = Random.Range(0, frontierTiles.Count);   //Generating a random number between 0 and the last element in the frontier

            Tile selectedTile = frontierTiles[index];   //Assigning the randomly selected tile in the frontier to the selected tile variable

            List<Tile> mazeNeighbors = new List<Tile>();    //List of neighbors of the current tile already in the maze.

            mazeTiles.Add(selectedTile);    //Adding that tile into the maze
            
            frontierTiles.Remove(selectedTile); //Removing that selected tile from frontier once it is in the maze.     

            int countMazeNeighbors = 0; //Counts the number of neighbors this tile already has in the maze.

            foreach(Tile tile in selectedTile.GetNeighbors())
            {
                if (mazeTiles.Contains(tile)) //Adds the neighboring tile into the mazeNeighbors list if it is already in the maze.
                {
                    countMazeNeighbors++;
                    mazeNeighbors.Add(tile);
                }
                else if(frontierTiles.Contains(tile))
                {
                    continue;   //Continue if the tile is inside the frontier.
                }
                else
                {
                    frontierTiles.Add(tile);    //Add the tile to the frontier otherwise.
                }
            }


            int mazeNeighborsIndex = Random.Range(0, mazeNeighbors.Count);  

            Tile selectedNeighbor = mazeNeighbors[mazeNeighborsIndex];  //Selects a random neighbor inside the maze


            Vector3 tileNeighborDistance = selectedNeighbor.GetPosition() - selectedTile.GetPosition();

            GameObject selectedTree;

            //If the neighbor is to the left
            if(tileNeighborDistance.x == -2.0f) //Remove all trees to the left of the current tile
            {
                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 1.0f, 0.0f, selectedTile.GetPosition().z - 0.75f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 1.0f, 0.0f, selectedTile.GetPosition().z -0.50f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 1.0f, 0.0f, selectedTile.GetPosition().z - 0.25f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 1.0f, 0.0f, selectedTile.GetPosition().z)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 1.0f, 0.0f, selectedTile.GetPosition().z + 0.25f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 1.0f, 0.0f, selectedTile.GetPosition().z + 0.50f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 1.0f, 0.0f, selectedTile.GetPosition().z + 0.75f)];
                selectedTree.SetActive(false);

            }
            //If the neighbor is to the right
            else if(tileNeighborDistance.x == 2.0f) //Remove all trees to the right of the current tile
            {   
                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 1.0f, 0.0f, selectedTile.GetPosition().z - 0.75f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 1.0f, 0.0f, selectedTile.GetPosition().z - 0.50f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 1.0f, 0.0f, selectedTile.GetPosition().z - 0.25f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 1.0f, 0.0f, selectedTile.GetPosition().z)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 1.0f, 0.0f, selectedTile.GetPosition().z + 0.25f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 1.0f, 0.0f, selectedTile.GetPosition().z + 0.50f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 1.0f, 0.0f, selectedTile.GetPosition().z + 0.75f)];
                selectedTree.SetActive(false);
            }
            //If the neighbor is below 
            else if(tileNeighborDistance.z == -2.0f)    //Remove all trees below the current tile.
            {
                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 0.75f, 0.0f, selectedTile.GetPosition().z - 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x -0.50f, 0.0f, selectedTile.GetPosition().z - 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 0.25f, 0.0f, selectedTile.GetPosition().z - 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x, 0.0f, selectedTile.GetPosition().z - 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 0.25f, 0.0f, selectedTile.GetPosition().z - 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 0.50f, 0.0f, selectedTile.GetPosition().z - 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 0.75f, 0.0f, selectedTile.GetPosition().z - 1.0f)];
                selectedTree.SetActive(false);
            }
            //If the neighbor is above
            else if(tileNeighborDistance.z == 2.0f) //Remove all trees above the current tile.
            {
                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 0.75f, 0.0f, selectedTile.GetPosition().z + 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 0.50f, 0.0f, selectedTile.GetPosition().z + 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x + 0.25f, 0.0f, selectedTile.GetPosition().z + 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x, 0.0f, selectedTile.GetPosition().z + 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 0.25f, 0.0f, selectedTile.GetPosition().z + 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 0.50f, 0.0f, selectedTile.GetPosition().z + 1.0f)];
                selectedTree.SetActive(false);

                selectedTree = trees[new Vector3(selectedTile.GetPosition().x - 0.75f, 0.0f, selectedTile.GetPosition().z + 1.0f)];
                selectedTree.SetActive(false);
            }
        }

    }

    //This method is called from the update function to record all trees acive and not active in the maze at the current timestep 
    void RecordCurrentActiveTrees()
    {
        //The key is the position of each tree and the value is a boolean that specifies if the tree is currently active or not
        Dictionary<Vector3, bool> treesActiveOrNot = new Dictionary<Vector3, bool>();   

        foreach(KeyValuePair<Vector3, GameObject> tree in trees)    //scan through the trees list and record that it is active in the treesActiveOrNot list.
        {
            if (tree.Value.activeSelf == true)
                treesActiveOrNot[tree.Key] = true;
            else treesActiveOrNot[tree.Key] = false;
        }

        activeTreesAtTimes.Add(time, treesActiveOrNot); //Add the treesActiveOrNot dictionary into the activeTreesAtTimes dictionary.
    }
}
