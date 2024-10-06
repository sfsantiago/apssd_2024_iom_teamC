function Main
{
  cls
  $JiraAccount = "<account>" # Example: https://proscrumdev.atlassian.net
  $JiraToken = "<email>:<token>" # Have to enable 2FA, and then paste the token into <token> to the left
  $JiraStoryId = "10001" # From https://<account>.atlassian.net/rest/api/latest/issuetype

  # Don't change anything below here

  $basicAuth = ("{0}:{1}" -f "",$JiraToken)
  $basicAuth = [System.Text.Encoding]::UTF8.GetBytes($basicAuth)
  $basicAuth = [System.Convert]::ToBase64String($basicAuth)
  $headers = @{Authorization=("Basic {0}" -f $basicAuth)}

  # ListProjects # Use this to simply list the projects in your Jira cloud account

  # Delete existing issues and populate the first 10 PBIs

  # ResetAndCreatePBIs1to10 "<project1>" # Example ResetAndCreatePBIs1to10 "AVENGERS"
  # ResetAndCreatePBIs1to10 "<project2>" # Example ResetAndCreatePBIs1to10 "REBELS"

  # Add additional PBIs in a later Sprint

  # AddPBIs11to14 "<project1>" # Example AddPBIs11to14 "AVENGERS"
  # AddPBIs11to14 "<project2>" # Example AddPBIs11to14 "REBELS"

  # Use ListIssues to do just that

  # ListIssues "<project1>" # Example ListIssues "AVENGERS"
}

function ResetAndCreatePBIs1to10([string]$project)
{
  DeleteIssues $project
  Write-Host "Creating" $project "Issues 1-10"

  CreateIssue $project "Make the game more readable (1)" "As a player, I want a clean user interface to help me find my way around.\n\n+Acceptance Criteria+\n* Hits and water should be shown in individual colors\n* A different color for messages\n* Individual game steps should be grouped visually\n* The player should be told exactly what the next possible steps are"
  CreateIssue $project "Indication when a ship has been sunk (2)" "As a player, I want to see which ships of my opponent I have sunk in order to identify which ships I still have to search for.\n\n+Acceptance Criteria+\n\n* Show only ships that have been sunk completely, not which ship was hit\n* Show me in a way that I can see which ships are left over"
  CreateIssue $project "Game does not end (3)" "+Steps to reproduce+\n\n* Start a new game\n* Sink all the computer's ships\n* The game does not end\n\n+Acceptance Criteria+\n\n* When all ships have been sunk, the player sees a message saying, 'You are the winner!' or 'You lost!'\n* The game shall end as soon as a player has no more ships left that are not sunk"
  CreateIssue $project "Validate ship placement (4)" "As a player, I want to make sure that I'm placing all ships according to the rules so that it's a fair game and I can't cheat.\n\n+Acceptance Criteria+\n\n* Ships must not overlap each other\n* All ships have the correct size\n* All ships have all positions in a horizontal or vertical row, gaps are not allowed\n* If the player tries to set an invalid position, a message appears"
  CreateIssue $project "You can shoot at positions outside the playing field (5)" "+Steps to reproduce+\n\n* Shoot at position A20\n* A 'Miss' is displayed.\n\n+Acceptance Criteria+\n\n* An appropriate message is displayed when shooting at a position that is outside the playing field\n* The player can shoot again when shooting at a position that is outside the playing field"
  CreateIssue $project "Computer shall place its ships randomly (6)" "As a player, I want the computer to place its ships randomly so that I don't know their position after a few games and  the game becomes more interesting.\n\n+Acceptance Criteria+\n\n* The ships must be placed differently for each game\n* The placement must comply with the rules\n* Ships must not cross each other\n* No ships outside the field\n* All ships have the correct size\n* All ships have all positions in a horizontal or vertical row, gaps are not allowed \n* The player cannot see the positions of the ships of the computer"
  CreateIssue $project "Computers should shoot smarter (7)" "As a player, I want the computer to shoot smarter so that the game becomes more challenging for me.\n\n+Acceptance Criteria+\n\n* Do not shoot at the same position several times\n* Searching for a hit in adjacent positions instead of shooting at a completely different location\n* Search for good shot positions based on remaining ships"
  CreateIssue $project "Make size of the field configurable (8)" "As a player, I want to be able to configure the size of the playing field in order to be able to influence the duration of the game.\n\n+Acceptance Criteria+\n\n* Non-square fields should be allowed\n* Limit board size to 26 in both directions, otherwise the letters are not sufficient\n* If board is too small to place all ships, a message should be displayed\n* The computer should also use the new board size for placement and shooting\n* I should be able to shoot at all configured fields"
  CreateIssue $project "Make ship positions changeable during placement (9)" "As a player, I want to be able to adjust the positions of my ships during the placement so that I can re-arrange them.\n\n+Acceptance Criteria+\n\n* The changed positions must also be valid\n* Changes should only be possible if the game has not yet been started\n* The position editing process should be intuitive for the player"
  CreateIssue $project "Two human players (10)" "As a player, I want to be able to play against other players to have fun together.\n\n+Acceptance Criteria+\n\n* Each player should be able to play on his computer\n* Playing against the computer should still be possible\n* There are always two players\n* If there are several players on my network, I want to be able to choose who I want to play against"
}

function AddPBIs11to14([string]$project)
{
  Write-Host "Adding" $project "Issues 11-14"

  CreateIssue $project "Computer should not shoot at the same position multiple times (11)" "As a player, I don't want the computer to shoot at the same position several times so that its chances of winning increase and the game becomes more challenging to me.\n\n+Acceptance Criteria+\n\n* The computer should not shoot again at a position it has used before\n* The computer shall fire on all positions of the playing field"
  CreateIssue $project "Computer is supposed to search for adjacent positions after a hit (12)" "As a player, I want the computer to try to sink my ship after a hit to increase its chances of winning and make the game more challenging for me.\n\n+Acceptance Criteria+\n\n* After a hit, the fields at the top / bottom / left / right of the hit should be fired upon\n* Immediately after another hit has been made on one of the adjacent fields, the orientation of the ship can be detected and this information should be used for the next shot\n* If no more hits are scored in the ship's direction and the ship is not yet sunk, then continue shooting in the other direction from the first hit\n* After the ship has been sunk, the next shot position is to be searched randomly on the whole field again\n* If two ships touch each other, the system must detect this and sink both ships"
  CreateIssue $project "Computer shall search for good shooting positions based on the remaining ships (13)" "As a player, I want the computer to look for the best possible shot positions to increase its chances of winning and make the game more challenging for me.\n\n+Acceptance Criteria+\n\n* Do not shoot in gaps where there is not enough space for the smallest vessel to be sunk\n* Consider gaps in horizontal and vertical direction"
  CreateIssue $project "Cool Happy Ending (14)" "As a player, I want a cool message or something like that when I win the game, so I'm motivated to play the game again.\n\n+Acceptance Criteria+\n\n* The message should be a positive surprise\n* The message should work on a standard computer and require no special equipment"
}

function CreateIssue([string]$project, [string]$summary, [string]$description)
{
  $project = $project.ToUpper()
  $resource = $JiraAccount + '/rest/api/latest/issue/'
  $body = '{"fields":{"project":{"key": "' + $project + '"},"summary": "' + $summary + '","description": "' + $description + '","issuetype":{"id": "' + $JiraStoryId + '"}}}';
  try {
    $response = Invoke-RestMethod -Uri $resource -Body $body -headers $headers -Method POST -ContentType 'application/json'
    Write-Host " " $summary
  }
  catch {
    echo $_.Exception|format-list -force
    return
  }
}

function ListIssues ([string]$project)
{
  write-host $project "Issues"
  $boardId = GetBoardID $project
  if ($boardId -gt 0)
  {
    $resource = $JiraAccount + '/rest/agile/latest/board/' + $boardId + '/backlog'
    $response = Invoke-RestMethod -Uri $resource -headers $headers -Method Get
    $response.issues | ForEach-Object {
      write-host " " $_.id $_.fields.issuetype.name ':' $_.fields.summary
    }
  }
}

function DeleteIssues ([string]$project)
{
  write-host "Deleting" $project "Issues"
  $boardId = GetBoardID $project
  if ($boardId -gt 0)
  {
    $resource = $JiraAccount + '/rest/agile/latest/board/' + $boardId + '/backlog'
    $response = Invoke-RestMethod -Uri $resource -headers $headers -Method Get
    $response.issues | ForEach-Object {
      $issueId = $_.id
      write-host " " $issueId
      $resource2 = $JiraAccount + '/rest/api/latest/issue/' + $issueId
      $response2 = Invoke-RestMethod -Uri $resource2 -headers $headers -Method Delete
    }
  }
}

function GetBoardID([string]$project)
{
  $boardId = 0
  $resource = $JiraAccount + '/rest/agile/latest/board'
  $response = Invoke-RestMethod -Uri $resource -headers $headers -Method Get
  $response.values | ForEach-Object {
    if ($_.name.Trim().toUpper().Equals($project.Trim().toUpper() + " BOARD"))
    {
      $boardId = $_.id
    }
  }
  return $boardId
}

function ListProjects
{
  $resource = $JiraAccount + '/rest/api/latest/project?expand'
  $response = Invoke-RestMethod -Uri $resource -headers $headers -Method Get
  write-host "Projects"
  write-host
  $response
}

Main