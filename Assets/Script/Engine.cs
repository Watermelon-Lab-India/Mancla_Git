using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mancala;

public enum Match_Type
{
	MT_None = 0,
	MT_PvsP = 1,
	MT_PvsCPU = 2,
	MT_PvsNET = 3
}

public enum Move_Input
{
    MI_None,
    MI_Player1,
    MI_Player2
}

public enum NetHuman_MoveInput
{
    NMI_NONE,
    NMI_Player1,
    NMI_Player2
}
public class Engine : MonoBehaviour {

	public GameObject[] tables;
	public Match_Type mtType = Match_Type.MT_None;

	public MancalaMatch match;

	public UIStoneManager uStoneManager;

    public NativeShare nativeShareObject;

    public int curSelTable = -1;

    public int[] coinsArray = { 100, 300, 500, 700, 900, 1100 };

	/// <summary>
	/// Start this instance.
	/// </summary>

	private static int timeLimit = 1000;		//1000				                // turn time in msec
    public static bool isTryPlaying = false;
	private Player pHuman = new HumanPlayer(Position.Top, timeLimit);
	private Player pTop = new BonzoPlayer(Position.Top, timeLimit);	// TOP player (MAX)
	private Player pBot = new mcw33Player(Position.Bottom, timeLimit);  // BOTTOM player	
    private Player pHuman1 = new HumanPlayer(Position.Bottom, timeLimit);
    private Player pNetHuman = new HumanPlayer(Position.Bottom, timeLimit);
    public Board b;			                                // playing surface
	private int move;

    int gameDifficulty = 0;
    int losingStreak = 0;

    bool isWaitingMyMove = false;
    public bool isPlayingGame = false;

    public bool isWaitingOtherJoinRoom = false, isFaceBookPlay = false;
    float waitingOtherEnterTime = 0f;
    Move_Input waitMoveInputFromPlayer;
    NetHuman_MoveInput waitNetInput = NetHuman_MoveInput.NMI_NONE;
    int waitMoveInput = 0;
    float gameOverTime = 0f;
	int myMove = -1, vsPlayerMove = -1, tmpNetPlayerMove = -1, netPlayerMove = -1;
	// Use this for initialization
	void Start () {
	}

	public int playGame(Player pTop, Player pBot, Position firstPlayer)
	{
		UnityEngine.Debug.Log ("game is playing now");

		b = new Board(firstPlayer);

		if (firstPlayer == Position.Top)
			UnityEngine.Debug.Log("Player " + pTop.getName() + " starts.");
		else
			UnityEngine.Debug.Log("Player " + pBot.getName() + " starts.");

		b.display();
		isPlayingGame = true;

		return 0;
	}

	public void StartGame(Match_Type matchtype, int tableId)
	{
        curSelTable = tableId;
        gameOverTime = 0f;

        //tables[curSelTable].transform.Find("Stones").gameObject.SetActive(true);
        for (int i = 0; i < 6; i++)
        {
            if (i == curSelTable)
            {
                tables[curSelTable].SetActive(true);
            }
            else
            {
                tables[i].SetActive(false);
            }
        }
        tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().InitGameArea();
        //uStoneManager.InitGameArea();

		mtType = matchtype;

		int result = 0;
        UnityEngine.Debug.Log("Engine StartGame");
        UnityEngine.Debug.Log ("start Game ---" + matchtype);

        float whoesStart = Random.Range(-1f, 1f);
        Position startPos = Position.Bottom;
        if(whoesStart >= 0)
        {
            startPos = Position.Top;
        }
        else
        {
            startPos = Position.Bottom;
        }

        if (this.gameObject.GetComponent<UIManager>().myPositionInfo)
        {
            pHuman = new HumanPlayer(Position.Top, timeLimit);
            pTop = new BonzoPlayer(Position.Top, timeLimit); // TOP player (MAX)
            pBot = new mcw33Player(Position.Bottom, timeLimit);  // BOTTOM player
            pHuman1 = new HumanPlayer(Position.Bottom, timeLimit);
        }
        else
        {
            pHuman = new HumanPlayer(Position.Bottom, timeLimit);
            pTop = new BonzoPlayer(Position.Top, timeLimit); // TOP player (MAX)
            pBot = new mcw33Player(Position.Top, timeLimit);  // BOTTOM player
            pHuman1 = new HumanPlayer(Position.Top, timeLimit);
        }
        switch (matchtype) 
		{
		case Match_Type.MT_PvsP:
			playGame (pHuman, pHuman1, startPos);
			break;
		case Match_Type.MT_PvsCPU:
			Debug.Log("Used:-");
            if(gameDifficulty == 0)
            {
				Debug.Log("0000");
                pBot = new mcw33Player(Position.Top, timeLimit, 1);  // BOTTOM player
            }
            else if(gameDifficulty == 1)
            {
				Debug.Log("1111");
                    pBot = new mcw33Player(Position.Top, timeLimit, 3);  // BOTTOM player
            }
            else if(gameDifficulty == 2)
            {
				Debug.Log("2222");
                    pBot = new mcw33Player(Position.Top, timeLimit, 4);  // BOTTOM player
            }                               
			playGame (pHuman, pBot, startPos);
			break;
		case Match_Type.MT_PvsNET:
                this.gameObject.GetComponent<UIManager>().playerInfo.totalMatchCount++;
                this.gameObject.GetComponent<UIManager>().playerInfo.coins -= coinsArray[tableId];
                if (this.gameObject.GetComponent<UIManager>().playerInfo.coins < 0)
                {
                    this.gameObject.GetComponent<UIManager>().playerInfo.coins = 0;
                }
                this.gameObject.GetComponent<UIManager>().SavePlayerInfo();
                playGame (pHuman, pBot, startPos);
			break;
		default:
			break;
		}

		UnityEngine.Debug.Log ("result ---" + result);

	}

    public void SetAIBOTDifficulty(int difficulty)
    {
        gameDifficulty = difficulty;
        if (difficulty == 0)
        {
//			int bestMove = 
        }
        else if(difficulty == 1)
        {

        }
        else if(difficulty == 2)
        {

        }
    }

    public void PlayGameWithRandomPlayer(int tableId)
    {
		
		
        gameOverTime = 0f;
        curSelTable = tableId;



    }

	public void BowlSelected(int num)
	{
        if(mtType == Match_Type.MT_PvsCPU)
        {
            if (isWaitingMyMove && !tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().stoneMovingState)//uStoneManager.stoneMovingState)
            {
                myMove = num;
            }
        }
        else if(mtType == Match_Type.MT_PvsP)
        {
            if(waitMoveInputFromPlayer == Move_Input.MI_Player1 && !tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().stoneMovingState)//uStoneManager.stoneMovingState)
            {
                myMove = num;
            }
            else if(waitMoveInputFromPlayer == Move_Input.MI_Player2 && !tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().stoneMovingState)//uStoneManager.stoneMovingState)
            {
                vsPlayerMove = num;
            }
        }
        else if(mtType == Match_Type.MT_PvsNET)
        {
            if(waitNetInput == NetHuman_MoveInput.NMI_Player1 && !CheckIsNotMoving())
            {
                myMove = num;

                this.gameObject.GetComponent<NetworkingManager>().SendMyMoveToOtherPlayer(7 + myMove);
            }
        }
	}


    public void OpponentSelectedBowl(int bowlId)
    {
        tmpNetPlayerMove = bowlId;
        if (mtType == Match_Type.MT_PvsNET)
        {
            if (waitNetInput == NetHuman_MoveInput.NMI_Player2 && CheckIsNotMoving())
            {
                netPlayerMove = tmpNetPlayerMove;
            }
        }
    }

    public bool CheckIsNotMoving()
    {
        if(tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().stoneMovingState)//uStoneManager.stoneMovingState)
        {
            return true;
        }

        for (int i = 0; i < 14; i++)
        { 
           // GameObject fObj = tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().gameObject.transform.Find(i.ToString()).gameObject;//uStoneManager.gameObject.transform.Find(i.ToString()).gameObject;
            GameObject fObj = tables[curSelTable].transform.Find("Stones").gameObject.transform.Find(i.ToString()).gameObject;
            for (int j = 0; j < fObj.transform.childCount; j++)
                {
                    if (fObj.transform.GetChild(j).gameObject.GetComponent<UIStonObjectController>().isMoving)
                    {
                        return true;
                    }
                }
            }

        if(mtType == Match_Type.MT_PvsCPU)
        {
            if (isWaitingMyMove)
            {

            }
            else
            {

            }
        }else if(mtType == Match_Type.MT_PvsP)
        {

        }

        return false;
    }

    float dTime = 0f;

    static public int gamePlayState = 0;

    public void PlayVSCPU()
    {
        gamePlayState = 1;
        if (this.gameObject.GetComponent<UIManager>().myPositionInfo && !b.gameOver())
        {
            if (b.whoseMove() == Position.Top)
            {
                isWaitingMyMove = true;

                if (!CheckIsNotMoving())
                {
                    this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(1);
                    if (myMove >= 0)
                    {
                        if (b.legalMove(myMove))
                        {
                            move = myMove;

                            b.makeMove(move, true);     // last parameter says to be chatty
                            b.display();
                            dTime = Time.time;
                            tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
                            // uStoneManager.BowlSelected(move);

                            UnityEngine.Debug.Log(pTop.getName() + " chooses move " + move);

                            isWaitingMyMove = false;

                            this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
                        }
                    }
                    myMove = -1;
                }
            }
            else 
            {
                isWaitingMyMove = false;
                if (!CheckIsNotMoving())// && Time.time - dTime > 1f)
                {
                    this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(0);
//                    StartCoroutine(WaitForDisplayWaiting());
                    if (Time.time - dTime > 1f)
                    {
                        move = pBot.chooseMove(b);
                        UnityEngine.Debug.Log(pBot.getName() + " chooses move " + move);

                        b.makeMove(move, true);     // last parameter says to be chatty
                        b.display();
                        dTime = Time.time;
                        tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
                        // uStoneManager.BowlSelected(move);
                        this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
                    }
                }
            }
        }
        else if(!this.gameObject.GetComponent<UIManager>().myPositionInfo && !b.gameOver())
        {
            if (b.whoseMove() == Position.Top)
            {
                isWaitingMyMove = false;
                if (!CheckIsNotMoving())// && Time.time - dTime > 1f)
                {
                    this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(0);
//                    StartCoroutine(WaitForDisplayWaiting());
                    if (Time.time - dTime > 1f)
                    {
                        move = pBot.chooseMove(b);
                        UnityEngine.Debug.Log(pBot.getName() + " chooses move " + move);

                        b.makeMove(move, true);     // last parameter says to be chatty
                        b.display();
                        dTime = Time.time;
                        //uStoneManager.BowlSelected(move);
                        tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
                        this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
                    }
                }

            }
            else
            {
                isWaitingMyMove = true;

                if (!CheckIsNotMoving())
                {
                    this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(1);
                    if (myMove >= 0)
                    {
                        if (b.legalMove(myMove))
                        {
                            move = myMove;

                            b.makeMove(move, true);     // last parameter says to be chatty
                            b.display();
                            dTime = Time.time;
                            tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
                            // uStoneManager.BowlSelected(move);

                            UnityEngine.Debug.Log(pTop.getName() + " chooses move " + move);

                            isWaitingMyMove = false;
                            this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
                        }
                    }
                    myMove = -1;
                }
            }
        }



        if (!CheckIsNotMoving() && b.gameOver())
        {
/*            if (gameOverTime <= 0f)
            {
                gameOverTime = Time.time;
            }*/
//            uStoneManager.MoveAllStonesToBox();
            isPlayingGame = false;
//            if (Time.time - gameOverTime >= 3f)
//            {
                if (this.gameObject.GetComponent<UIManager>().myPositionInfo)
                {
                    Debug.Log("GameOver cpu");
                    if (b.winner() == Position.Top)
                    {
                        this.gameObject.GetComponent<UIManager>().DisplayWinningState(1, b.scoreTop(), b.scoreBot());
                        UnityEngine.Debug.Log("Player " + pTop.getName() + " (TOP) wins " + b.scoreTop() + " to " + b.scoreBot());
					
						Debug.Log(" cpu B Winner Top:-");
						
                    }
                    else if (b.winner() == Position.Bottom)
                    {
                        this.gameObject.GetComponent<UIManager>().DisplayWinningState(-1, b.scoreTop(), b.scoreBot());
                        UnityEngine.Debug.Log("Player " + pBot.getName() + " (BOTTOM) wins " + b.scoreBot() + " to " + b.scoreTop());

						//computer win
                    }
                    else
                    {
                        this.gameObject.GetComponent<UIManager>().DisplayWinningState(0, 24, 24);
                        UnityEngine.Debug.Log("A tie!");
                    }
                }
                else
                {

                    Debug.Log("GameOver player");
                    if (b.winner() == Position.Bottom)
                        {
                            if (this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().oppoName.text.Equals("CPU") == false)
                            {
                                Debug.Log("not cpu");
                                losingStreak = 0;
                                this.gameObject.GetComponent<UIManager>().playerInfo.winCount++;
                                this.gameObject.GetComponent<UIManager>().SendAchievementProgressToServer();
                                this.gameObject.GetComponent<UIManager>().SendGameWinInfoToGooglePlay();
                                this.gameObject.GetComponent<UIManager>().playerInfo.coins = this.gameObject.GetComponent<UIManager>().playerInfo.coins + (coinsArray[curSelTable]*2);
                                this.gameObject.GetComponent<UIManager>().SavePlayerInfo();
                            }                            
                            
                            this.gameObject.GetComponent<UIManager>().DisplayWinningState(1, b.scoreBot(), b.scoreTop());
                            UnityEngine.Debug.Log("Player " + pTop.getName() + " (TOP) wins " + b.scoreBot() + " to " + b.scoreTop());
						    Debug.Log(" cpu player win bootm:-");
                        }
                        else if (b.winner() == Position.Top)
                        {
                            if (this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().oppoName.text.Equals("CPU") == false)
                            {
                                Debug.Log("not cpu");
                                losingStreak++;
                                this.gameObject.GetComponent<UIManager>().playerInfo.matchLoseCount++;
                                if (this.gameObject.GetComponent<UIManager>().playerInfo.coins < 0)
                                {
                                    this.gameObject.GetComponent<UIManager>().playerInfo.coins = 0;
                                }

                                //this.gameObject.GetComponent<UIManager>().playerInfo.coins -= coinsArray[curSelTable];

                                this.gameObject.GetComponent<UIManager>().SavePlayerInfo();
                            }
                            this.gameObject.GetComponent<UIManager>().DisplayWinningState(-1, b.scoreBot(), b.scoreTop());
                            UnityEngine.Debug.Log("Player " + pBot.getName() + " (BOTTOM) wins " + b.scoreBot() + " to " + b.scoreTop());
                        }
                        else
                        {
                            this.gameObject.GetComponent<UIManager>().DisplayWinningState(0, 24, 24);
                            UnityEngine.Debug.Log("A tie!");
                        }
                    }
                }     
//        }
    }

    public IEnumerator WaitForDisplayWaiting()
    {
        yield return new WaitForSeconds(0.5f);
        move = pBot.chooseMove(b);
        UnityEngine.Debug.Log(pBot.getName() + " chooses move " + move);

        b.makeMove(move, true);     // last parameter says to be chatty
        b.display();
        dTime = Time.time;
        //uStoneManager.BowlSelected(move);
        tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
        this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
    }

    public void PlayVSPlayer()
    {
        gamePlayState = 2;
        if (this.gameObject.GetComponent<UIManager>().myPositionInfo)
        {
            if (b.whoseMove() == Position.Top)
            {
                waitMoveInputFromPlayer = Move_Input.MI_Player1;

                if (!CheckIsNotMoving() && Time.time - dTime > 0.5f)
                {
                    this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(1);
                    if (myMove >= 0)
                    {
                        if (b.legalMove(myMove))
                        {
                            move = myMove;

                            b.makeMove(move, true);     // last parameter says to be chatty
                            b.display();
                            dTime = Time.time;
                            //uStoneManager.BowlSelected(move);
                            tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
                            UnityEngine.Debug.Log(pTop.getName() + " chooses move " + move);

                            waitMoveInputFromPlayer = Move_Input.MI_None;

                            this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
                        }
                    }
                    myMove = -1;
                }
            }
            else if (!CheckIsNotMoving() && Time.time - dTime > 0.5f)
            {
                this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(0);
                waitMoveInputFromPlayer = Move_Input.MI_Player2;
                if (!CheckIsNotMoving() && Time.time - dTime > 0.5f)
                {
                    if (vsPlayerMove >= 0)
                    {
                        if (b.legalMove(vsPlayerMove))
                        {
                            move = vsPlayerMove;

                            //                        move = pHuman1.chooseMove(b);
                            UnityEngine.Debug.Log(pBot.getName() + " chooses move " + move);

                            b.makeMove(move, true);     // last parameter says to be chatty
                            b.display();
                            dTime = Time.time;
                            tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
                            //uStoneManager.BowlSelected(move);

                            waitMoveInputFromPlayer = Move_Input.MI_None;
                            vsPlayerMove = -1;

                            this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
                        }
                    }
                }
            }
        }
        else
        {
            if (b.whoseMove() == Position.Top)
            {
                waitMoveInputFromPlayer = Move_Input.MI_Player2;
                if (!CheckIsNotMoving() && Time.time - dTime > 0.5f)
                {
                    this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(0);
                    if (vsPlayerMove >= 0)
                    {
                        if (b.legalMove(vsPlayerMove))
                        {
                            move = vsPlayerMove;

                            //                        move = pHuman1.chooseMove(b);
                            UnityEngine.Debug.Log(pBot.getName() + " chooses move " + move);

                            b.makeMove(move, true);     // last parameter says to be chatty
                            b.display();
                            dTime = Time.time;
                            // uStoneManager.BowlSelected(move);
                            tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
                            waitMoveInputFromPlayer = Move_Input.MI_None;
                            vsPlayerMove = -1;
                            this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
                        }
                    }
                }
            }
            else if (!CheckIsNotMoving() && Time.time - dTime > 0.5f)
            {
                this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(1);
                waitMoveInputFromPlayer = Move_Input.MI_Player1;

                if (!CheckIsNotMoving() && Time.time - dTime > 0.5f)
                {
                    if (myMove >= 0)
                    {
                        if (b.legalMove(myMove))
                        {
                            move = myMove;

                            b.makeMove(move, true);     // last parameter says to be chatty
                            b.display();
                            dTime = Time.time;
                            tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
                            //uStoneManager.BowlSelected(move);

                            UnityEngine.Debug.Log(pTop.getName() + " chooses move " + move);

                            waitMoveInputFromPlayer = Move_Input.MI_None;
                            this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
                        }
                    }
                    myMove = -1;
                }                
            }
        }

        if (!CheckIsNotMoving() && b.gameOver())
        {

            isPlayingGame = false;

//            uStoneManager.MoveAllStonesToBox();
/*
            if(gameOverTime <= 0f)
            {
                gameOverTime = Time.time;
            }*/
//            if(Time.time - gameOverTime > 3f)
//            {
                if (this.gameObject.GetComponent<UIManager>().myPositionInfo)
                {
                    Debug.Log("GameOver PlayVSPlayer");
                    
                    if (b.winner() == Position.Top)
                    {
                        this.gameObject.GetComponent<UIManager>().DisplayWinningState(1, b.scoreTop(), b.scoreBot());
                        UnityEngine.Debug.Log("Player " + pTop.getName() + " (TOP) wins " + b.scoreTop() + " to " + b.scoreBot());
						
						/*this.gameObject.GetComponent<UIManager>().playerInfo.coins += 200; 
						this.gameObject.GetComponent<UIManager>().SavePlayerInfo();*/
						
						Debug.Log("B Winner Top:-");

	                }
                    else if (b.winner() == Position.Bottom)
                    {
                        this.gameObject.GetComponent<UIManager>().DisplayWinningState(-1, b.scoreTop(), b.scoreBot());
                        //                this.gameObject.GetComponent<UIManager>().DisplayWinningState(-1);
                        UnityEngine.Debug.Log("Player " + pBot.getName() + " (BOTTOM) wins " + b.scoreBot() + " to " + b.scoreTop());
						
						
                    }
                    else
                    {
                        this.gameObject.GetComponent<UIManager>().DisplayWinningState(0, 24, 24);
                        //                this.gameObject.GetComponent<UIManager>().DisplayWinningState(0);
                        UnityEngine.Debug.Log("A tie!");
                    }
                }
                else
                {
                    Debug.Log("GameOver PlayVSPlayer 1");
                    if (b.winner() == Position.Bottom)
                    {
                        this.gameObject.GetComponent<UIManager>().DisplayWinningState(1, b.scoreBot(), b.scoreTop());
                        UnityEngine.Debug.Log("Player " + pTop.getName() + " (TOP) wins " + b.scoreBot() + " to " + b.scoreTop());
						/*this.gameObject.GetComponent<UIManager>().playerInfo.coins += 200;
						this.gameObject.GetComponent<UIManager>().SavePlayerInfo();*/
						Debug.Log("player Winner bottm:-");
                    }
                    else if (b.winner() == Position.Top)
                    {
                        this.gameObject.GetComponent<UIManager>().DisplayWinningState(-1, b.scoreBot(), b.scoreTop());
                        //                this.gameObject.GetComponent<UIManager>().DisplayWinningState(-1);
                        UnityEngine.Debug.Log("Player " + pBot.getName() + " (BOTTOM) wins " + b.scoreBot() + " to " + b.scoreTop());

					}
                    else
                    {
                        this.gameObject.GetComponent<UIManager>().DisplayWinningState(0, 24, 24);
                        //                this.gameObject.GetComponent<UIManager>().DisplayWinningState(0);
                        UnityEngine.Debug.Log("A tie!");
                    }
                }
            }
//        }
    }

    //play game online mode
    public void PlayGameOnline(int tableId, string playerName)
    {
        isTryPlaying = true;
        isFaceBookPlay = false;
        gameOverTime = 0f;
        curSelTable = tableId;
        StartCoroutine(WaitTimeToConnect(tableId, playerName));
    }

    IEnumerator WaitTimeToConnect(int tableId, string playerName)
    {
        int wTime = Random.Range(1, 6);
        Debug.LogError("--wait time-0----" + wTime);
        yield return new WaitForSeconds(0f);
        this.gameObject.GetComponent<NetworkingManager>().ConnectToServerver(tableId, playerName);
    }

    public void SendJoinRequest(string roomId)
        {
        this.gameObject.GetComponent<NetworkGlobalManager>().SendJoinRoomRequest(roomId);
        }

    public void OpponentAcceptedChallenge()
        {
        this.gameObject.GetComponent<NetworkingManager>().ConnectToSerververSpecial(curSelTable, this.gameObject.GetComponent<UIManager>().playerInfo.playerName, specRoomId);
        }

    public void PlayGameByCode(int tableId, string playerName,string roomcode)
    {
        specRoomId = roomcode;
        isTryPlaying = true;
        isFaceBookPlay = false;
        gameOverTime = 0f;
        curSelTable = tableId;
        StartCoroutine(WaitTimeToConnectBycode(tableId, playerName));
    }

    IEnumerator WaitTimeToConnectBycode(int tableId, string playerName)
    {
        int wTime = Random.Range(1, 6);
        Debug.LogError("--wait time-0----" + wTime);
        yield return new WaitForSeconds(0f);
        this.gameObject.GetComponent<NetworkingManager>().JoinChallengedPlayer(specRoomId, playerName);
    }

    string specRoomId;

    //play game with online player in facebook friendlist
    public void PlayGameWithFaceBook(int tableId, string roomId)
    {
        isTryPlaying = true;
        gameOverTime = 0f;
        curSelTable = tableId;
        specRoomId = roomId;
        this.gameObject.GetComponent<Engine>().GamePlayRequest(roomId);

//        this.gameObject.GetComponent<NetworkingManager>().ConnectToSerververSpecial(tableId, this.gameObject.GetComponent<UIManager>().playerInfo.playerName, roomId);
    }

    //send chat text to the opps player
    public void SendMyChatToOtherPlayer(string chatText)
    {
        if(mtType == Match_Type.MT_PvsNET)
        {
            this.gameObject.GetComponent<NetworkingManager>().SendMyChat(chatText);
        }  
    }
    
    //send emotion to other player
    public void SendEmotionToOtherPlayer(int id)
    {
        if(mtType == Match_Type.MT_PvsNET)
        {
            this.gameObject.GetComponent<NetworkingManager>().SendMyEmotion(id);
        }
    }

    //this fuction is called when the connection disconned because of any reasone
    public void OnNetWorkDisconnected()
    {
        Debug.Log("isconnecting false-----------------------------------------");
        isTryPlaying = false;
        this.gameObject.GetComponent<UIManager>().CouldConnectionFailed();
        this.gameObject.GetComponent<NetworkingManager>().isConnecting = false;
    }

    //this function is for facebook play
    public void WaitingOppentesFaceBook()
    {
        isPlayingGame = false;
        waitingOtherEnterTime = Time.time;
//		Debug.Log("Waiting Time:-"+waitingOtherEnterTime );
        isWaitingOtherJoinRoom = true;
        isFaceBookPlay = true;
        //        uStoneManager.InitGameArea();

//        this.gameObject.GetComponent<UIManager>().playerInfo.coins -= coinsArray[curSelTable];
        this.gameObject.GetComponent<UIManager>().SavePlayerInfo();
        this.gameObject.GetComponent<UIManager>().CouldConnectionFailed();
        this.gameObject.GetComponent<UIManager>().timmerObject.SetActive(false);
        this.gameObject.GetComponent<UIManager>().SetPlayingGameState(curSelTable);
        /*tables[curSelTable].transform.Find("Stones").gameObject.SetActive(true);*/
        for (int i = 0; i < 6; i++)
        {
            if (i == curSelTable)
            {
                tables[curSelTable].SetActive(true);
            }
            else
            {
                tables[i].SetActive(false);
            }
        }
        tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().InitGameArea();
        UnityEngine.Debug.Log("Engine WaitingOpponentsFacebook");
    }



    //this function is called when the player created the room and waiting the opponent connection
    public void WaitingOppentes()
    {
        isPlayingGame = false;
        waitingOtherEnterTime = Time.time;
        isWaitingOtherJoinRoom = true;

        this.gameObject.GetComponent<UIManager>().SavePlayerInfo();
        this.gameObject.GetComponent<UIManager>().CouldConnectionFailed();
        this.gameObject.GetComponent<UIManager>().timmerObject.SetActive(false);
        
        this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().oppoName.text = "Opponent";
        this.gameObject.GetComponent<UIManager>().SetPlayingGameState(curSelTable);
        for (int i = 0; i < 6; i++)
        {
            if (i == curSelTable)
            {
                tables[curSelTable].SetActive(true);
            }
            else
            {
                tables[i].SetActive(false);
            }
        }
        tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().InitGameArea();
        UnityEngine.Debug.Log("Engine WaitingOpponents");
    }

    public void WaitingOppentesWithCode()
    {
        isPlayingGame = false;
        waitingOtherEnterTime = Time.time;
        isWaitingOtherJoinRoom = true;

        this.gameObject.GetComponent<UIManager>().SavePlayerInfo();
        this.gameObject.GetComponent<UIManager>().CouldConnectionFailed();
        this.gameObject.GetComponent<UIManager>().timmerObject.SetActive(false);

        this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().oppoName.text = "Opponent";

        //this.gameObject.GetComponent<UIManager>().SetPlayingGameState(curSelTable);

        //for (int i = 0; i < 6; i++)
        //{
        //    if (i == curSelTable)
        //    {
        //        tables[curSelTable].SetActive(true);
        //    }
        //    else
        //    {
        //        tables[i].SetActive(false);
        //    }
        //}
        //tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().InitGameArea();
        UnityEngine.Debug.Log("Engine WaitingOpponents");
    }

    public void SetGamePlayCode()
    {
        this.gameObject.GetComponent<UIManager>().SetPlayingGameState(curSelTable);

        for (int i = 0; i < 6; i++)
        {
            if (i == curSelTable)
            {
                tables[curSelTable].SetActive(true);
            }
            else
            {
                tables[i].SetActive(false);
            }
        }
        tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().InitGameArea();
        UnityEngine.Debug.Log("Engine set gameplay code");
    }

    //this function is called when the player successed with connecting the the random room
    public void ConnectionSuccessed(string otherPlayerName)
    {

    }

    //this function is called when the match start with the other player
    //the param shows who will start the game 
    //param {0,1} 
    public void OpponentEnteredGame(int starter, string otherPlayerName)
    {
        isWaitingOtherJoinRoom = false;

        this.gameObject.GetComponent<UIManager>().playerInfo.coins -= coinsArray[curSelTable];
        if (this.gameObject.GetComponent<UIManager>().playerInfo.coins < 0)
            this.gameObject.GetComponent<UIManager>().playerInfo.coins = 0;
        this.gameObject.GetComponent<UIManager>().SavePlayerInfo();


        //tables[curSelTable].transform.Find("Stones").gameObject.SetActive(true);
        for (int i = 0; i < 6; i++)
        {
            if (i == curSelTable)
            {
                tables[curSelTable].SetActive(true);
            }
            else
            {
                tables[i].SetActive(false);
            }
        }
        tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().InitGameArea();

        mtType = Match_Type.MT_PvsNET;
        UnityEngine.Debug.Log("Engine OpponentEnteredGame");

        pHuman = new HumanPlayer(Position.Bottom, timeLimit);
        pNetHuman = new HumanPlayer(Position.Top, timeLimit);

        if(starter == 1)
        {
            playGame(pHuman, pNetHuman, Position.Bottom);
        }
        else
        {
            playGame(pHuman, pNetHuman, Position.Top);
        }
        
        this.gameObject.GetComponent<UIManager>().SetOpponentInformation(otherPlayerName);

        this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(starter);

    }

    public void SetOpponentInformationCode(string otherPlName)
    {
        isWaitingOtherJoinRoom = false;
        //        isFaceBookPlay = false;
        for (int i = 0; i < 6; i++)
        {
            if (i == curSelTable)
            {
                tables[curSelTable].SetActive(true);
            }
            else
            {
                tables[i].SetActive(false);
            }
        }

        this.gameObject.GetComponent<UIManager>().CouldConnectionFailed();
      //  this.gameObject.GetComponent<UIManager>().SetOpponentInformation(otherPlName);
    }

    //set other player information when I join the game
    public void SetOpponentInformation(string otherPlName)
    {
        isWaitingOtherJoinRoom = false;
//        isFaceBookPlay = false;
        for (int i = 0; i < 6; i++)
        {
            if (i == curSelTable)
            {
                tables[curSelTable].SetActive(true);
            }
            else
            {
                tables[i].SetActive(false);
            }
        }

        this.gameObject.GetComponent<UIManager>().CouldConnectionFailed();
        this.gameObject.GetComponent<UIManager>().SetOpponentInformation(otherPlName);
    }

    public void AcceptPlayWithFacebookPlayer(string chPl)
    {
        isTryPlaying = true;
        isFaceBookPlay = true;
        curSelTable = 0;
        this.gameObject.GetComponent<NetworkingManager>().JoinChallengedPlayer(chPl, this.gameObject.GetComponent<UIManager>().playerInfo.playerName);
    }

    public void GamePlayRequest(string friendID)
    {
//        this.gameObject.GetComponent<NetworkingManager>().SendPlayMatchRequest(friendID);
          this.gameObject.GetComponent<NetworkGlobalManager>().SendGamePlayRequest(friendID);
    }
    //player vs net human
    public void PlayAgainstNetHuman()
    {
        gamePlayState = 3;
        if (b.whoseMove() == Position.Top)
        {
            waitNetInput = NetHuman_MoveInput.NMI_Player2;
            if (!CheckIsNotMoving() && Time.time - dTime > 0.5f)
            {
//              this.gameObject.GetComponent<UIManager>().StartTimerCount();
                this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(0);
                if (netPlayerMove >= 0)
                {
                    if (b.legalMove(netPlayerMove))
                    {
                        move = netPlayerMove;

                        //                        move = pHuman1.chooseMove(b);
                        UnityEngine.Debug.Log(pNetHuman.getName() + " chooses move " + move);

                        b.makeMove(move, true);     // last parameter says to be chatty
                        b.display();
                        dTime = Time.time;
                        tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
                        //uStoneManager.BowlSelected(move);

                        waitNetInput = NetHuman_MoveInput.NMI_NONE;
                        netPlayerMove = -1;
                        tmpNetPlayerMove = -1;

                        this.gameObject.GetComponent<UIManager>().StopCounting();
                        this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
                    }
                }
            }
        }
        else if (!CheckIsNotMoving() && Time.time - dTime > 0.5f)
        {
            waitNetInput = NetHuman_MoveInput.NMI_Player1;

            if (!CheckIsNotMoving() && Time.time - dTime > 0.5f)
            {
//                this.gameObject.GetComponent<UIManager>().StartTimerCount();
                this.gameObject.GetComponent<UIManager>().SetWhoesMoveToUI(1);

                if (myMove >= 0)
                {
                    if (b.legalMove(myMove))
                    {
                        move = myMove;

                        b.makeMove(move, true);     // last parameter says to be chatty
                        b.display();
                        dTime = Time.time;
                        tables[curSelTable].transform.Find("Stones").gameObject.GetComponent<UIStoneManager>().BowlSelected(move);
                        //uStoneManager.BowlSelected(move);

                        UnityEngine.Debug.Log(pTop.getName() + " chooses move " + move);

                        waitNetInput = NetHuman_MoveInput.NMI_NONE;
                        this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();
                        this.gameObject.GetComponent<UIManager>().StopCounting();
                    }
                }
                myMove = -1;
            }
        }

        if (!CheckIsNotMoving() && b.gameOver())
        {
            isPlayingGame = false;

 //           uStoneManager.MoveAllStonesToBox();
/*
            if (gameOverTime <= 0f)
            {
                gameOverTime = Time.time;
            }*/
//            if (Time.time - gameOverTime > 3f)
//            {
                if (b.winner() == Position.Bottom)
                {
                    Debug.Log("Winner");
                    this.gameObject.GetComponent<UIManager>().playerInfo.winCount++;
	                this.gameObject.GetComponent<UIManager>().SendAchievementProgressToServer();
	                this.gameObject.GetComponent<UIManager>().SendGameWinInfoToGooglePlay();
	                this.gameObject.GetComponent<UIManager>().playerInfo.coins = this.gameObject.GetComponent<UIManager>().playerInfo.coins + (coinsArray[curSelTable]*2);
	                this.gameObject.GetComponent<UIManager>().SavePlayerInfo();
	                this.gameObject.GetComponent<UIManager>().DisplayWinningState(1, b.scoreBot(), b.scoreTop());
	                UnityEngine.Debug.Log("Player " + pTop.getName() + " (TOP) wins " + b.scoreTop() + " to " + b.scoreBot());
                }
                else if (b.winner() == Position.Top)
                {
                    this.gameObject.GetComponent<UIManager>().playerInfo.matchLoseCount ++;
	                /*if (this.gameObject.GetComponent<UIManager>().playerInfo.coins < 0)
	                    {
	                    this.gameObject.GetComponent<UIManager>().playerInfo.coins = 0;
	                    }

					this.gameObject.GetComponent<UIManager>().playerInfo.coins -= coinsArray[curSelTable];*/

	               	 this.gameObject.GetComponent<UIManager>().SavePlayerInfo();
	               	this.gameObject.GetComponent<UIManager>().DisplayWinningState(-1, b.scoreBot(), b.scoreTop());
	                    //                this.gameObject.GetComponent<UIManager>().DisplayWinningState(-1);
	                    UnityEngine.Debug.Log("Player " + pBot.getName() + " (BOTTOM) wins " + b.scoreBot() + " to " + b.scoreTop());
                }
                else
                {
                    this.gameObject.GetComponent<UIManager>().DisplayWinningState(0, 24, 24);
                    //                this.gameObject.GetComponent<UIManager>().DisplayWinningState(0);
                    UnityEngine.Debug.Log("A tie!");
                }
            }
//        }
    }

    static public bool isconnectWithBot = false;

    public void JoinWithBot()
    {
		Debug.Log("Joinwithbot");
        isconnectWithBot = true;
        gamePlayState = 3;
        isWaitingOtherJoinRoom = false;
        isFaceBookPlay = false;
        this.gameObject.GetComponent<NetworkingManager>().LeaveRoom();

        this.gameObject.GetComponent<UIManager>().playerInfo.coins -= coinsArray[curSelTable];
        if (this.gameObject.GetComponent<UIManager>().playerInfo.coins < 0)
             this.gameObject.GetComponent<UIManager>().playerInfo.coins = 0;
        this.gameObject.GetComponent<UIManager>().SavePlayerInfo();
        mtType = Match_Type.MT_PvsCPU;

        int wincnt = PlayerPrefs.GetInt("winCnt");
        if (wincnt <= 5)
        {
            gameDifficulty = 0;
        }
        else if (wincnt >= 6 && wincnt <= 10)
        {
            if (losingStreak >= 2)
            {
                gameDifficulty = 0;
            }
            else 
            {
                gameDifficulty = 1;
            }
        }
        else 
        {
            if (losingStreak == 2)
            {
                gameDifficulty = 1;
            }
            else if(losingStreak >2)
            {
                gameDifficulty = 0;
            }
            else 
            {
                gameDifficulty = 2;
            }
            
        }
        
        int i = Random.Range(0, 30);
        string otherPlayerName = UIManager.randNames[i];
        this.gameObject.GetComponent<UIManager>().SetOpponentInformation(otherPlayerName);

        StartGame(mtType, curSelTable);
    }
    
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isTryPlaying = false;
            isWaitingOtherJoinRoom = false;
            isPlayingGame = false;
            Debug.Log("isconnecting false-----------------------------------------");
            this.gameObject.GetComponent<NetworkingManager>().isConnecting = false;
            this.gameObject.GetComponent<NetworkingManager>().LeaveRoom();

            //if (UIManager._instance.game_Mode == 1)
            //{
            //    this.gameObject.GetComponent<UIManager>().ReturnToHome();
            //}
        }

        if (UIManager._instance.game_Mode == 1)
        {
            //if (isWaitingOtherJoinRoom && Time.time - waitingOtherEnterTime >= 30f)  //25f // atul change //default 10
            //{
            //    isWaitingOtherJoinRoom = false;
            //    Debug.Log("code game Play");
            //    this.gameObject.GetComponent<UIManager>().ReturnToHome();
            //}
        }
        else
        {
            if (isWaitingOtherJoinRoom && Time.time - waitingOtherEnterTime >= 10f)  //25f // atul change //default 10
            {
                if (isFaceBookPlay)
                {
                    Debug.Log("Facebook Play");
                    this.gameObject.GetComponent<UIManager>().ReturnToHome();
                }
                else
                {
                    Debug.Log("JoinWithBot");
                    JoinWithBot();
                }
            }
        }
		if(isPlayingGame) {
            if (mtType == Match_Type.MT_PvsCPU)
            {
                PlayVSCPU();
            }
            else if(mtType == Match_Type.MT_PvsP)
            {
                PlayVSPlayer();
            }
            else if(mtType == Match_Type.MT_PvsNET)
            {
                if (mtType == Match_Type.MT_PvsNET)
                {
                    if (waitNetInput == NetHuman_MoveInput.NMI_Player2 && !CheckIsNotMoving())
                    {
                        netPlayerMove = tmpNetPlayerMove;
                    }
                }
                PlayAgainstNetHuman();
            }
		}
	}
}
