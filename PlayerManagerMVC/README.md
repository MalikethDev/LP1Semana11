# PlayerManagerMVC

## UML Class Diagram

classDiagram
class PlayerView {
+ShowMenu()
+DisplayPlayers(IEnumerable~Player~)
+GetNewPlayerInfo() Tuple
+GetSortOrder() PlayerOrder
+GetMinimumScore() int
}

class PlayerController {
-List~Player~ players
-PlayerView view
-IComparer~Player~ compareByName
-IComparer~Player~ compareByNameReverse
+Run()
-InsertPlayer()
-ListPlayers()
-ListPlayersWithScoreGreaterThan()
-SortPlayers()
}

PlayerController --> PlayerView
PlayerController --> Player