# PlayerManagerMVC

## UML Class Diagram

```mermaid
classDiagram
    class Player { 
        +Name: string 
        +Score: int 
        +ToString(): string 
        +CompareTo(Player): int 
    }
    class PlayerView {
        +ShowMenu()
        +DisplayPlayers(IEnumerable~Player~)
        +GetNewPlayerInfo(): (string, int)
        +GetSortOrder(): PlayerOrder
        +GetMinimumScore(): int
        +WaitForKey()
        +DisplayGoodbye()
        +DisplayError(string)
    }

    class PlayerController {
        -playerList: List~Player~
        -view: PlayerView
        -compareByName: IComparer~Player~
        -compareByNameReverse: IComparer~Player~
        +Start()
        -InsertPlayer()
        -ListPlayers()
        -ListPlayersWithScoreGreaterThan()
        -SortPlayerList()
        -LoadPlayersFromFile(string)
    }

    class CompareByName {
        -ord: bool
        +Compare(Player, Player): int
    }

    PlayerController --> PlayerView
    PlayerController --> Player
    PlayerController --> CompareByName