# Battleship PBIs Description

[Back to the Trainer Guide](TrainerGuide.md)
## Purpose of this document
This document will provide you with some background information for each of the PBIs from the Battleship case study.
The case study unfolds its full potential if you understand the specific learning opportunities behind small little
details of each of the PBIs.

### PBI 1 - Make the game more readable
* Usually teams do not clarify with the Product Owner which colors they should use. If they do you could give different
teams different specification which might be A/B testing.
	* Blue = miss, Red = hit, Yellow = message (Blue and red will be hard to read on the projector)
	* Green = positive for player, Red = negative for player (will be tricky later when adding multi-player mode)
* It is not clearly specified what a message is and teams usually make a best guess on it. 
You can define them as text which asks user do take an action - that's the reason why you want them in color.  
Example:  
`Please enter the positions for the Aircraft Carrier (size: 5)`  
`Enter position 1 of 5 (i.e A3): `  
Here only the second line should be colored.
* Acceptance criteria `Individual game steps should be grouped visually` should raise questions. 
It means that after player and computer placed a shot there should be a visual separation before the next round starts
e.g. a horizontal line. This is important to get an better overview while using the history feature to recap the game progress.
What, you have not found the great history feature yet? (scrollbar on console window)
* Acceptance criteria `The player should be told exactly what the next possible steps are` is more a 
request for verification. We want that gamers can play the game without reading documentation. Check if we need more clarification.

### PBI 2 - Indication when a ship has been sunk
* Give freedom to the Development Team how to achieve this. But ensure that it fulfills the goal 
('to identify which ships I still have to search for') and it is universally understood.
* Request that the size of the ships is clear from the output as feedback during Sprint Review
* Hopefully there will be some questions about when to print this message. It could be after each round, after each hit
or after a ship has sunk or when the gamer entered a specific command. If it was not discussed before, you can 
complain that you expected something different during Sprint Review.

### PBI 3 - Game does not end
* Check if the messages are exactly the same as requested in the acceptance criteria.
* How will the team demo this? How well are they prepared? Does it take ages and gets boring for the stakeholders?
You can give a tip and say 'When I do presentations, I have all the necessary positions in the clipboard and just paste them to the console window'.
* Ask if the computer could win as well and how they can demo it or how this has been tested.
* Only accept the version for Sprint Review which will finally delivered to our customers, no prepared demo versions. 
* Teams could enter the Sprint Review with a instance of the game with a prepared state.

### PBI 4 - Validate ship placement
* This is a great example on dependencies and how to handle them but also about false dependencies.
* It seems that this PBI shares a lot of functionality with PBI #6. How to handle this during estimation?
* Usually you change the order and let teams implement PBI #6 first which change the order. Watch how teams react to this.
* This is a false dependency because it sounds very similar to PBI #6 but the implementations will be very different.
* If you want to use this PBI for learnings from Sprint 3 like Swarming, TDD and Architecture you should appreciate
suggestions on changing the current mode to place ships to something like starting-point and direction but explain
for the learnings, you'll stick with the current approach.
* There should be some discussion about details raised by the team. Do we validate each single position or the whole ship? etc. 
* Request to have an information about the rule which has been violated for the gamer.

### PBI 5 - You can shoot at positions outside the playing field
* Use this PBI to fill up sprints when needed, no specific learnings here.

### PBI 6 - Computer shall place its ships randomly
* This is an interesting one because teams think it is easy but it turns out to be so complex that most teams will not finish this within a sprint.
* Raise the question how confident the team is about being able to finish this. While discussing you might come up with 
a simpler solution which is acceptable as a MVP for the Product Owner. You could have 5 static board configurations
and pick one of them randomly. This might raise some discussions because to implement the final solution you will have
to throw away most of the code you implemented for this simple solution. Is that bad? 
* The random placement raises some additional challenges for testing. 

### PBI 7 - Computers should shoot smarter
* This PBI is there to explain how to split PBIs.
* You can suggest splitting out a separate PBI for each acceptance criteria (see PBIs 11-13)

### PBI 8 - Make size of the field configurable
* This is a stopgap if teams need more work :-)
* You could accept to limit the minimal size to 5x5 to make it easier to ensure all ships can be placed.

### PBI 9 - Make ship positions changeable during placement
* This is a stopgap, teams usually will not have enough time to implement it.

### PBI 10 - Two human players
* This PBI needs to be split up. 
* An MVP could be to have two players on the same computer. But then one additional acceptance criteria would that gamers
cannot see the ship placements of their opponents.

### PBI 11 - Computer should not shoot at the same position multiple times
* This PBI is created when splitting up PBI #7. You should not hand it out before the splitting process.
* It is a good opportunity to us an Unit Test.

### PBI 12 - Computer is supposed to search for adjacent positions after a hit
* This PBI is created when splitting up PBI #7. You should not hand it out before the splitting process.
* It shows greatly how you get more details and insights by splitting PBIs.
* Maybe you have to split this again because it still is relatively complex for a short sprint.

### PBI 13 - Computer shall search for good shooting positions based on the remaining ships
* This PBI is created when splitting up PBI #7. You should not hand it out before the splitting process.
* This is a stopgap, teams usually will not have enough time to implement it.

### PBI 14 - Cool Happy Ending
* This PBI is for the last sprint. Implementing and presenting it is a lot of fun and gives a positive feeling for the last sprint.
* Explain that the goal behind this PBI is to motivate gamers to play the game again and to tell their friends about it.
* You as a Product Owner don't know what is possible within a sprint but we need something to be ready by the end of the sprint
Therefore close collaboration between the Development Team and Product Owner might be the best way to get out the highest
possible value. It could be sound, animation, something funny. Throw in suggestions during the sprint to help the 
Development Team to improve the current solution.
