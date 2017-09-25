using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ActionMaster {

	public static int[] chessBoard = new int[64];	// declares and initializes the Chess Board.

	public static bool[] attackedByWhite = new bool[64];	//TODO update this array of booleans with information about every location on the board with the information: is that location attacked by a white piece.
	public static bool[] attackedByBlack = new bool[64];	//TODO update this array of booleans with information about every location on the board with the information: is that location attacked by a black piece.



	public static List<int> AvailableMoves (int pos)
	{	// takes in a position and returns a list of possible moves

		int north = 8;
		int east = 1;
		int south = -north;
		int west = -east;

		int nPos = north + pos;
		int ePos = east + pos;
		int sPos = south + pos;
		int wPos = west + pos;

		int nePos = north + ePos;
		int sePos = south + ePos;
		int swPos = south + wPos;
		int nwPos = north + wPos;

		int nnePos = north + nePos;
		int enePos = east + nePos;

		int esePos = east + sePos;
		int ssePos = south + sePos;

		int sswPos = south + swPos;
		int wswPos = west + swPos;

		int wnwPos = west + nwPos;
		int nnwPos = north + nwPos;

		List<int> aMoves = new List<int> ();

//		for (int i = 0; i < chessBoard.Length; i++) {	// TODO delete this forloop when you are capable of sucessfully checking for moves.
//			if (GameManager.myState == GameManager.States.WhiteTurn) {
//				if (chessBoard [i] <= 6) {
//					aMoves.Add (i);
//				}
//			} else if (GameManager.myState == GameManager.States.BlackTurn) {
//				if (chessBoard [i] == 0 || chessBoard [i] >= 7) {
//					aMoves.Add (i);
//				}
//			}
//		}

		if (chessBoard [pos] == 1) {	// if pos is a white king
			if (chessBoard [nPos] == 0 || chessBoard [nPos] >= 7) {	// check nPos and add to aMoves list if the move is valid
				if (!attackedByBlack [nPos]) {
					aMoves.Add (nPos);
				}
			}
			if (chessBoard [ePos] == 0 || chessBoard [ePos] >= 7) {	// check ePos and add to aMoves list if the move is valid
				if (!attackedByBlack [ePos]) {
					aMoves.Add (ePos);
				}
			}
			if (chessBoard [sPos] == 0 || chessBoard [sPos] >= 7) {	// check sPos and add to aMoves list if the move is valid
				if (!attackedByBlack [sPos]) {
					aMoves.Add (sPos);
				}
			}
			if (chessBoard [wPos] == 0 || chessBoard [wPos] >= 7) {	// check wPos and add to aMoves list if the move is valid
				if (!attackedByBlack [wPos]) {
					aMoves.Add (wPos);
				}
			}
		}
		if (chessBoard[pos] == 2) {	// if white queen

		}
		if (chessBoard[pos] == 3) {	// if white castle

		}
		if (chessBoard[pos] == 4) {	// if white bishop

		}
		if (chessBoard[pos] == 5) {	// if white knight
			if (pos <= 46 && pos % 8 <= 6) {
				if (chessBoard[nnePos] == 0 || chessBoard[nnePos] >= 7) {
					aMoves.Add(nnePos);
				}
			}
			if (pos <= 53 && pos % 8 <= 5) {
				if (chessBoard[enePos] == 0 || chessBoard[enePos] >= 7) {
					aMoves.Add(enePos);
				}
			}
			if (pos >= 8 && pos % 8 <= 5) {
				if (chessBoard[esePos] == 0 || chessBoard[esePos] >= 7) {
					aMoves.Add(esePos);
				}
			}
			if (pos >= 16 && pos % 8 <= 6) {
				if (chessBoard[ssePos] == 0 || chessBoard[ssePos] >= 7) {
					aMoves.Add(ssePos);
				}
			}
			if (pos >= 17 && pos % 8 >= 1) {
				if (chessBoard[sswPos] == 0 || chessBoard[sswPos] >= 7) {
					aMoves.Add(sswPos);
				}
			}
			if (pos >= 10 && pos % 8 >= 2) {
				if (chessBoard[wswPos] == 0 || chessBoard[wswPos] >= 7) {
					aMoves.Add(wswPos);
				}
			}
			if (pos <= 55 && pos % 8 >= 2) {
				if (chessBoard[wnwPos] == 0 || chessBoard[wnwPos] >= 7) {
					aMoves.Add(wnwPos);
				}
			}
			if (pos <= 47 && pos % 8 >= 1) {
				if (chessBoard[nnwPos] == 0 || chessBoard[nnwPos] >= 7) {
					aMoves.Add(nnwPos);
				}
			}
		}
		if (chessBoard [pos] == 6) {	// if white pawn
			if (chessBoard [nPos] == 0) {	// check nPos and add to aMoves list if the move is valid
				aMoves.Add(nPos);
				if (pos / 8 == 1) {
					if (chessBoard [north + nPos] == 0) {
						aMoves.Add(north + nPos);
					}
				}
			}
			if (chessBoard [nePos] >= 7) {	// if there is a black piece on nePos
				aMoves.Add (nePos);
			}
			if (chessBoard [nwPos] >= 7) {	// if there is a black piece on nwPos
				aMoves.Add (nwPos);
			}
		}
		if (chessBoard[pos] == 7) {	// if black king

		}
		if (chessBoard[pos] == 8) {	// if black queen

		}
		if (chessBoard[pos] == 9) {	// if black castle

		}
		if (chessBoard[pos] == 10) {	// if black bishop

		}
		if (chessBoard[pos] == 11) {	// if black knight
			if (pos <= 46 && pos % 8 <= 6) {
				if (chessBoard[nnePos] <= 6) {
					aMoves.Add(nnePos);
				}
			}
			if (pos <= 53 && pos % 8 <= 5) {
				if (chessBoard[enePos] <= 6) {
					aMoves.Add(enePos);
				}
			}
			if (pos >= 8 && pos % 8 <= 5) {
				if (chessBoard[esePos] <= 6) {
					aMoves.Add(esePos);
				}
			}
			if (pos >= 16 && pos % 8 <= 6) {
				if (chessBoard[ssePos] <= 6) {
					aMoves.Add(ssePos);
				}
			}
			if (pos >= 17 && pos % 8 >= 1) {
				if (chessBoard[sswPos] <= 6) {
					aMoves.Add(sswPos);
				}
			}
			if (pos >= 10 && pos % 8 >= 2) {
				if (chessBoard[wswPos] <= 6) {
					aMoves.Add(wswPos);
				}
			}
			if (pos <= 55 && pos % 8 >= 2) {
				if (chessBoard[wnwPos] <= 6) {
					aMoves.Add(wnwPos);
				}
			}
			if (pos <= 47 && pos % 8 >= 1) {
				if (chessBoard[nnwPos] <= 6) {
					aMoves.Add(nnwPos);
				}
			}
		}
		if (chessBoard[pos] == 12) {	// if black pawn

		}

//		 TODO for nnPos, nnnPos, nnnnPos etc
//		 if (north + nPos < chessBoard.Length) chessBoard[north + nPos].ToList();

//		if (chessBoard[pos] == 5 || chessBoard[pos] == 11) {	// if there is a knight on the position pos.
//			if (pos <= 46 && pos % 8 <= 6) {
//				aMoves.Add(i);
//			}
//			if (pos <= 53 && pos % 8 <= 5) {
//				chessBoard[j];
//			}
//			if (pos >= 8 && pos % 8 <= 5) {
//				chessBoard[k];
//			}
//			if (pos >= 16 && pos % 8 <= 6) {
//				chessBoard[l];
//			}
//			if (pos >= 17 && pos % 8 >= 1) {
//				chessBoard[m];
//			}
//			if (pos >= 10 && pos % 8 >= 2) {
//				chessBoard[n];
//			}
//			if (pos <= 55 && pos % 8 >= 2) {
//				chessBoard[o];
//			}
//			if (pos <= 47 && pos % 8 >= 1) {
//				chessBoard[p];
//			}
//		}
		return aMoves;
	}
}