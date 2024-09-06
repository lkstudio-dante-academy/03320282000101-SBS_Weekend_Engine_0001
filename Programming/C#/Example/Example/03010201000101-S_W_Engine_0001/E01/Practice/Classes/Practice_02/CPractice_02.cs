//#define P02_01
//#define P02_02
//#define P02_03
//#define P02_04
//#define P02_05
//#define P02_06
//#define P02_07
//#define P02_08
//#define P02_09
//#define P02_10
//#define P02_11
//#define P02_12
#define P02_13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._03010201000101_S_W_Engine_0001.E01.Practice.Classes.Practice__02
{
	internal class CPractice_02
	{
#if P02_07
		/** 값을 설정한다 */
		public static void SetupVals(int[,] a_oVals) {
			for(int i = 0; i < a_oVals.GetLength(0); ++i) {
				for(int j = 0; j < a_oVals.GetLength(1); ++j) {
					a_oVals[i, j] = (i * a_oVals.GetLength(1)) + j;
				}
			}
		}

		/** 정답 여부를 검사한다 */
		public static bool IsAnswer(int[,] a_oVals, int[,] a_oAnswerVals) {
			for(int i = 0; i < a_oVals.GetLength(0); ++i) {
				for(int j = 0; j < a_oVals.GetLength(1); ++j) {
					// 값이 다를 경우
					if(a_oVals[i, j] != a_oAnswerVals[i, j]) {
						return false;
					}
				}
			}

			return true;
		}

		/** 값을 재배치한다 */
		public static void ShuffleVals(int[,] a_oVals) {
			var oRandom = new Random((int)DateTime.Now.Ticks);

			for(int i = 0; i < a_oVals.GetLength(0); ++i) {
				for(int j = 0; j < a_oVals.GetLength(1); ++j) {
					int nRow = oRandom.Next(0, a_oVals.GetLength(0));
					int nCol = oRandom.Next(0, a_oVals.GetLength(1));

					int nTemp = a_oVals[i, j];
					a_oVals[i, j] = a_oVals[nRow, nCol];
					a_oVals[nRow, nCol] = nTemp;
				}
			}
		}

		/** 값을 출력한다 */
		public static void PrintVals(int[,] a_oVals) {
			for(int i = 0; i < a_oVals.GetLength(0); ++i) {
				for(int j = 0; j < a_oVals.GetLength(1); ++j) {
					Console.Write("{0,4}", (a_oVals[i, j] <= 0) ? " " : $"{a_oVals[i, j]}");
				}

				Console.WriteLine();
			}
		}
#elif P02_08
		/** 피보나치 값을 반환한다 */
		public static int GetFibonacci(int a_nVal) {
			if(a_nVal <= 1) {
				return (a_nVal <= 0) ? 0 : 1;
			}

			return GetFibonacci(a_nVal - 1) + GetFibonacci(a_nVal - 2);
		}
#elif P02_09
		/** 값을 설정한다 */
		public static void SetupVals(int[,] a_oVals) {
			for(int i = 0; i < a_oVals.GetLength(0); ++i) {
				for(int j = 0; j < a_oVals.GetLength(1); ++j) {
					a_oVals[i, j] = (i * a_oVals.GetLength(1)) + j;
				}
			}
		}

		/** 값을 회전한다 */
		public static void RotateVals(int[,] a_oVals, int a_nDirection) {
			for(int i = 0; i < a_oVals.GetLength(0) / 2; ++i) {
				for(int j = i; j < a_oVals.GetLength(1) - (i + 1); ++j) {
					int nLBRow = a_oVals.GetLength(0) - (j + 1);
					int nLBCol = i;

					int nLTRow = i;
					int nLTCol = j;

					int nRTRow = j;
					int nRTCol = a_oVals.GetLength(1) - (i + 1);

					int nRBRow = a_oVals.GetLength(0) - (i + 1);
					int nRBCol = a_oVals.GetLength(1) - (j + 1);

					// 왼쪽 방향 일 경우
					if(a_nDirection <= 0) {
						int nTemp = a_oVals[nLBRow, nLBCol];
						a_oVals[nLBRow, nLBCol] = a_oVals[nLTRow, nLTCol];
						a_oVals[nLTRow, nLTCol] = a_oVals[nRTRow, nRTCol];
						a_oVals[nRTRow, nRTCol] = a_oVals[nRBRow, nRBCol];
						a_oVals[nRBRow, nRBCol] = nTemp;
					} else {
						int nTemp = a_oVals[nLBRow, nLBCol];
						a_oVals[nLBRow, nLBCol] = a_oVals[nRBRow, nRBCol];
						a_oVals[nRBRow, nRBCol] = a_oVals[nRTRow, nRTCol];
						a_oVals[nRTRow, nRTCol] = a_oVals[nLTRow, nLTCol];
						a_oVals[nLTRow, nLTCol] = nTemp;
					}
				}
			}
		}

		/** 값을 출력한다 */
		public static void PrintVals(int[,] a_oVals) {
			for(int i = 0; i < a_oVals.GetLength(0); ++i) {
				for(int j = 0; j < a_oVals.GetLength(1); ++j) {
					Console.Write("{0,4}", a_oVals[i, j]);
				}

				Console.WriteLine();
			}
		}
#elif P02_10
		/** 맵을 출력한다 */
		public static void PrintMap(char[,] a_oMap) {
			for(int i = 0; i < a_oMap.GetLength(0); ++i) {
				for(int j = 0; j < a_oMap.GetLength(1); ++j) {
					Console.Write("{0}", a_oMap[i, j]);
				}

				Console.WriteLine();
			}
		}

		/** 경로를 탐색한다 */
		public static bool FindPath(char[,] a_oMap, int a_nPosX, int a_nPosY) {
			// 배열을 벗어났을 경우
			if(a_nPosX < 0 || a_nPosX >= a_oMap.GetLength(1) || a_nPosY < 0 || a_nPosY >= a_oMap.GetLength(0)) {
				return false;
			}

			// 이동이 불가능 할 경우
			if(a_oMap[a_nPosY, a_nPosX] == '*' || a_oMap[a_nPosY, a_nPosX] == '#') {
				return false;
			}

			// 목적지에 도착했을 경우
			if(a_oMap[a_nPosY, a_nPosX] == 'E') {
				return true;
			}

			char chLetter = a_oMap[a_nPosY, a_nPosX];
			a_oMap[a_nPosY, a_nPosX] = (chLetter == 'S') ? 'S' : '*';

			var oXOffsets = new int[4] {
				1, -1, 0, 0
			};

			var oYOffsets = new int[4] {
				0, 0, 1, -1
			};

			for(int i = 0; i < oXOffsets.Length; ++i) {
				// 경로 탐색에 성공했을 경우
				if(FindPath(a_oMap, a_nPosX + oXOffsets[i], a_nPosY + oYOffsets[i])) {
					return true;
				}
			}

			a_oMap[a_nPosY, a_nPosX] = chLetter;
			return false;
		}
#elif P02_11
		/** 숫자를 출력한다 */
		public static void PrintDigits(string a_oStr) {
			var oDigitStrs = new string[] {
				"*****     * ***** ***** *   * ***** ***** ***** ***** *****",
				"*   *     *     *     * *   * *     *         * *   * *   *",
				"*   *     *     *     * *   * *     *         * *   * *   *",
				"*   *     * ***** ***** ***** ***** *****     * ***** *****",
				"*   *     * *         *     *     * *   *     * *   *     *",
				"*   *     * *         *     *     * *   *     * *   *     *",
				"*****     * ***** *****     * ***** *****     * ***** *****"
			};

			for(int i = 0; i < oDigitStrs.Length; ++i) {
				for(int j = 0; j < a_oStr.Length; ++j) {
					// 숫자 일 경우
					if(char.IsDigit(a_oStr[j])) {
						int nDigit = a_oStr[j] - '0';
						Console.Write("{0} ", oDigitStrs[i].Substring((nDigit * 5) + nDigit, 5));
					}
				}

				Console.WriteLine();
			}
		}
#elif P02_12
		/** 하노이 탑 결과를 출력한다 */
		public static void PrintHanoiTower(int a_nNumVals) {
			DoPrintHanoiTower(a_nNumVals, 1, 3);
		}

		/** 하노이 탑 결과를 출력한다 */
		private static void DoPrintHanoiTower(int a_nVal, int a_nSrc, int a_nDest) {
			if(a_nVal <= 1) {
				Console.WriteLine("{0} 번 원반 : {1} -> {2} 이동", a_nVal, a_nSrc, a_nDest);
			} else {
				DoPrintHanoiTower(a_nVal - 1, a_nSrc, 6 - (a_nSrc + a_nDest));

				Console.WriteLine("{0} 번 원반 : {1} -> {2} 이동", a_nVal, a_nSrc, a_nDest);
				DoPrintHanoiTower(a_nVal - 1, 6 - (a_nSrc + a_nDest), a_nDest);
			}
		}
#elif P02_13
		/** 우선 순위를 반환한다 */
		public static int GetPriority(char a_chOperator)
		{
			switch(a_chOperator)
			{
				case '*':
				case '/':
					return 1;
				case '+':
				case '-':
					return 2;
			}

			return 3;
		}

		/** 수식 결과를 반환한다 */
		public static decimal GetCalcResult(string a_oPostfix)
		{
			var oStack = new Stack<decimal>();

			for(int i = 0; i < a_oPostfix.Length; ++i)
			{
				// 피연산자 일 경우
				if(char.IsDigit(a_oPostfix[i]))
				{
					oStack.Push(a_oPostfix[i] - '0');
				}
				else
				{
					decimal dmRhs = oStack.Pop();
					decimal dmLhs = oStack.Pop();

					switch(a_oPostfix[i])
					{
						case '+':
							oStack.Push(dmLhs + dmRhs);
							break;
						case '-':
							oStack.Push(dmLhs - dmRhs);
							break;
						case '*':
							oStack.Push(dmLhs * dmRhs);
							break;
						case '/':
							oStack.Push(dmLhs / dmRhs);
							break;
					}
				}
			}

			return oStack.Pop();
		}

		/** 중위 -> 후위 표기법으로 변환한다 */
		public static string ConvertInToPostfix(string a_oInfix)
		{
			var oStack = new Stack<char>();
			var oStrBuilder = new StringBuilder();

			string oValidOperators = "+-*/()";

			for(int i = 0; i < a_oInfix.Length; ++i)
			{
				// 공백 일 경우
				if(char.IsWhiteSpace(a_oInfix[i]))
				{
					continue;
				}

				// 피연산자 일 경우
				if(char.IsDigit(a_oInfix[i]))
				{
					oStrBuilder.Append(a_oInfix[i]);
				}
				// 유효한 연산자 일 경우
				else if(oValidOperators.Contains(a_oInfix[i]))
				{
					// 닫힌 괄호 일 경우
					if(a_oInfix[i] == ')')
					{
						while(oStack.Count > 0)
						{
							char chOperator = oStack.Pop();

							// 열린 괄호 일 경우
							if(chOperator == '(')
							{
								break;
							}

							oStrBuilder.Append(chOperator);
						}
					}
					else
					{
						while(oStack.Count > 0)
						{
							int nPriority01 = GetPriority(a_oInfix[i]);
							int nPriority02 = GetPriority(oStack.Peek());

							// 현재 연산자 우선 순위가 높을 경우
							if(nPriority01 < nPriority02)
							{
								break;
							}

							oStrBuilder.Append(oStack.Pop());
						}

						oStack.Push(a_oInfix[i]);
					}
				}
			}

			while(oStack.Count > 0)
			{
				oStrBuilder.Append(oStack.Pop());
			}

			return oStrBuilder.ToString();
		}
#endif // #elif P02_01

		/** 초기화 */
		public static void Start(string[] args)
		{
#if P02_01
			/*
			 * 과제 2 - 1
			 * - 입력 한 단에 해당하는 구구단 출력하기
			 * 
			 * Ex)
			 * 단 입력 : 3
			 * 
			 * =====> 결과 <=====
			 * 3 * 1 = 3
			 * 3 * 2 = 6
			 * 3 * 3 = 9
			 * 
			 * ... 이하 생략
			 */
			Console.Write("단 입력 : ");
			int nVal = int.Parse(Console.ReadLine());

			Console.WriteLine("\n=====> 결과 <=====");

			for(int i = 1; i < 10; ++i) {
				Console.WriteLine("{0} * {1} = {2}", nVal, i, nVal * i);
			}
#elif P02_02
			/*
			 * 과제 2 - 2
			 * - 최소/최대 값 탐색하기
			 * - 길이가 10 인 배열을 선언 후 랜덤한 값 (0 ~ 99) 으로 해당 배열 초기화 후 탐색 수행
			 * 
			 * PS.
			 * 랜덤 값 생성 방법은 연습 문제 1 - 5 참고
			 * 
			 * Ex)
			 * =====> 배열 요소 <=====
			 * 10, 25, 33, 34, 46, 89, 90, 98, 0, 11
			 * 
			 * 최소 : 0
			 * 최대 : 98
			 */
			var oVals = new int[10];
			var oRandom = new Random((int)DateTime.Now.Ticks);

			for(int i = 0; i < oVals.Length; ++i) {
				oVals[i] = oRandom.Next(0, 100);
			}

			Console.WriteLine("=====> 배열 요소 <=====");

			for(int i = 0; i < oVals.Length; ++i) {
				Console.Write("{0}, ", oVals[i]);
			}

			int nMinVal = int.MaxValue;
			int nMaxVal = int.MinValue;

			for(int i = 0; i < oVals.Length; ++i) {
				nMinVal = (nMinVal < oVals[i]) ? nMinVal : oVals[i];
				nMaxVal = (nMaxVal > oVals[i]) ? nMaxVal : oVals[i];
			}

			Console.WriteLine("\n\n최소 : {0}", nMinVal);
			Console.WriteLine("최대 : {0}", nMaxVal);
#elif P02_03
			/*
			 * 과제 2 - 3
			 * - 문자열 길이를 입력 받은 후 해당 길이만큼의 랜덤한 알파벳 문자로 구성된 문자열 생성하기
			 * 
			 * Ex)
			 * 문자열 길이 입력 : 10
			 * 결과 : ActXiOjdDQ
			 */
			Console.Write("문자열 길이 입력 : ");
			int nLength = int.Parse(Console.ReadLine());

			var oRandom = new Random();
			var oLetters = new char[nLength];

			for(int i = 0; i < oLetters.Length; ++i) {
				char chLetter = (oRandom.Next(0, 2) <= 0) ? 'A' : 'a';
				oLetters[i] = (char)(chLetter + oRandom.Next(0, ('Z' - 'A') + 1));
			}

			Console.WriteLine("결과 : {0}", string.Concat(oLetters));
#elif P02_04
			/*
			 * 과제 2 - 4
			 * - 개수를 입력 받아 해당 개수만큼 피보나치 수열 출력하기
			 * 
			 * Ex)
			 * 개수 입력 : 8
			 * 
			 * =====> 결과 <=====
			 * 0, 1, 1, 2, 3, 5, 8, 13
			 */
			Console.Write("개수 입력 : ");
			int nNumVals = int.Parse(Console.ReadLine());

			Console.WriteLine("\n=====> 결과 <=====");

			int nVal = 0;
			int nPrevVal = 0;
			int nNextVal = 1;

			for(int i = 0; i < nNumVals; ++i) {
				Console.Write("{0}, ", nVal);

				nPrevVal = nVal;
				nVal = nNextVal;
				nNextVal = nVal + nPrevVal;
			}

			Console.WriteLine();
#elif P02_05
			/*
			 * 과제 2 - 5
			 * - 문자열을 입력 받아 해당 문자열을 뒤집은 결과 출력하기
			 * - 문자열의 출력 순서를 변경하는 것이 아니라 문자열 자체를 뒤집어야한다 (Reverse 메서드 사용 금지)
			 * 
			 * Ex)
			 * 문자열 입력 : ABCD한글
			 * 결과 : 글한DCBA
			 */
			Console.Write("문자열 입력 : ");
			string oStr = Console.ReadLine();

			var oLetters = oStr.ToArray();

			for(int i = 0; i < oLetters.Length / 2; ++i) {
				char chTemp = oLetters[i];
				oLetters[i] = oLetters[oLetters.Length - (i + 1)];
				oLetters[oLetters.Length - (i + 1)] = chTemp;
			}

			Console.WriteLine("결과 : {0}", string.Concat(oLetters));
#elif P02_06
			/*
			 * 과제 2 - 6
			 * - 문자열을 입력 받아 회문 여부 검사하기
			 * 
			 * Ex)
			 * 문자열 입력 : AABB
			 * 결과 : 회문 or 회문 아님
			 */
			Console.Write("문자열 입력 : ");
			string oStr = Console.ReadLine();

			int nLeft = 0;
			int nRight = oStr.Length - 1;

			while(nLeft < nRight && oStr[nLeft] == oStr[nRight]) {
				nLeft += 1;
				nRight -= 1;
			}

			Console.WriteLine("결과 : {0}", (nLeft >= nRight) ? "회문입니다" : "회문이 아닙니다");
#elif P02_07
			/*
			 * 과제 2 - 7
			 * - 슬라이드 퍼즐 게임 제작하기
			 * - 프로그램이 시작되면 너비, 높이를 입력 받는다 (단, 입력 가능한 최대 너비, 높이는 각각 50 으로 제한)
			 * - 숫자 0 부터 입력한 (너비 x 높이) - 1 까지 값을 부여한 2 차원 배열 생성하기 (숫자 0 은 공백을 의미한다)
			 * - 생성 된 배열의 각 요소를 무작위로 재배치 후 해당 배열을 올바른 순서로 맞출 경우 게임 종료
			 * 
			 * Ex)
			 * 너비, 높이 입력 : 3 3
			 * 
			 * 1 4 5									1 2 3
			 * 3 2 6	<-	공백 주변의 요소를 옮겨서		4 5 6	<-	순서로 맞출 경우 게임 종료
			 * 8 7										7 8
			 * 
			 * 위치 입력 : 2 1	<- 행, 열 순으로 위치를 입력 받는다
			 * 
			 * 1 4 5
			 * 3 2 6
			 * 8   7
			 * 
			 * 입력 받은 위치의 상/하/좌/우 에 공백이 존재 할 경우 공백과 해당 위치에 존재하는 숫자를 바꾼다 (단, 입력 받은 
			 * 위치 주변에 공백이 없을 경우에는 무시)
			 * 
			 * 위의 과정을 반복해서 올바른 순서로 값이 배치 될 경우 게임을 종료한다
			 */
			Console.Write("너비, 높이 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int nWidth = int.Parse(oTokens[0]);
			int nHeight = int.Parse(oTokens[1]);

			var oVals = new int[nHeight, nWidth];
			var oAnswerVals = new int[nHeight, nWidth];

			SetupVals(oVals);
			Array.Copy(oVals, oAnswerVals, oVals.Length);

			ShuffleVals(oVals);

			while(!IsAnswer(oVals, oAnswerVals)) {
				PrintVals(oVals);

				Console.Write("\n위치 입력 : ");
				oTokens = Console.ReadLine().Split();

				int nRow = int.Parse(oTokens[0]);
				int nCol = int.Parse(oTokens[1]);

				var oXOffsets = new int[4] {
					1, -1, 0, 0
				};

				var oYOffsets = new int[4] {
					0, 0, 1, -1
				};

				for(int i = 0; i < oXOffsets.Length; ++i) {
					int nNearRow = nRow + oYOffsets[i];
					int nNearCol = nCol + oXOffsets[i];

					// 배열을 벗어났을 경우
					if(nNearCol < 0 || nNearCol >= oVals.GetLength(1) || nNearRow < 0 || nNearRow >= oVals.GetLength(0)) {
						continue;
					}

					// 공백이 존재 할 경우
					if(oVals[nNearRow, nNearCol] == 0) {
						int nTemp = oVals[nRow, nCol];
						oVals[nRow, nCol] = oVals[nNearRow, nNearCol];
						oVals[nNearRow, nNearCol] = nTemp;

						break;
					}
				}
			}
#elif P02_08
			/*
			 * 과제 2 - 8
			 * - 개수를 입력 받아 해당 개수만큼 피보나치 수열 출력하기 (단, 재귀 호출 사용)
			 * 
			 * 개수 입력 : 8
			 * 
			 * =====> 결과 <=====
			 * 0, 1, 1, 2, 3, 5, 8, 13
			 */
			Console.Write("개수 입력 : ");
			int nNumVals = int.Parse(Console.ReadLine());

			Console.WriteLine("\n=====> 결과 <=====");

			for(int i = 0; i < nNumVals; ++i) {
				Console.Write("{0}, ", GetFibonacci(i));
			}

			Console.WriteLine();
#elif P02_09
			/*
			 * 과제 2 - 9
			 * - 2 차원 배열 회전하기
			 * - 2 차원 배열 크기를 입력 받은 후 0 부터 차례대로 각 요소를 초기화한다
			 * - 회전 방향을 입력 받아 해당 방향으로 90 도 회전 된 결과 출력하기 (단, 회전 과정에서 별도의 배열을 사용하는 것은 불가)
			 * - 단, 2 차원 배열 크기는 정방 크기로 제한 (Ex. 3 x 3, 5 x 5, 등등...)
			 * 
			 * Ex)
			 * 크기 입력 : 3
			 * 
			 * =====> 배열 요소 <=====
			 * 0 1 2
			 * 3 4 5
			 * 6 7 8
			 * 
			 * 회전 방향 입력 (0 : 왼쪽 방향, 1 : 오른쪽 방향) : 0
			 * 
			 * Case 1. 왼쪽 회전
			 * =====> 배열 요소 - 회전 후 <=====
			 * 2 5 8
			 * 1 4 7
			 * 0 3 6
			 * 
			 * Case 2. 오른쪽 회전
			 * =====> 배열 요소 - 회전 후 <=====
			 * 6 3 0
			 * 7 4 1
			 * 8 5 2
			 */
			Console.Write("크기 입력 : ");
			int nSize = int.Parse(Console.ReadLine());

			var oVals = new int[nSize, nSize];
			SetupVals(oVals);

			Console.WriteLine("\n=====> 배열 요소 <=====");
			PrintVals(oVals);

			Console.Write("\n회전 방향 입력 (0 : 왼쪽, 1 : 오른쪽) : ");
			int nDirection = int.Parse(Console.ReadLine());

			RotateVals(oVals, nDirection);

			Console.WriteLine("\n=====> 배열 요소 - 회전 후 <=====");
			PrintVals(oVals);
#elif P02_10
			/*
			 * 과제 2 - 10
			 * - 미로를 탈출 할 수 있는 경로를 탐색하는 함수 제작하기 (단, 탈출하기 위한 미로는 동적으로 생성하기 않고 
			 * 미리 제작)
			 * 
			 * Ex)
			 * =====> 탐색 전 <=====
			 * ####S####
			 * #       #
			 * ###  ####
			 * #   #   #
			 * ##      #
			 * ## # #  #
			 * ##E######
			 * 
			 * Case 1. 탐색에 성공했을 경우
			 * =====> 탐색 후 <=====
			 * ####S####
			 * #   *   #
			 * ###**####
			 * #  *#   #
			 * ##**    #
			 * ##*# #  #
			 * ##E######
			 * 
			 * Case 2. 탐색에 실패했을 경우
			 * =====> 탐색 후 <=====
			 * ####S####
			 * #       #
			 * ###  ####
			 * #   #   #
			 * ##      #
			 * ## # #  #
			 * ##E######
			 * 
			 * S (시작 위치) 부터 E (종료 위치) 까지 갈 수 있는 경로를 탐색 후 결과를 출력한다 (단, 탈출이 불가능 할 
			 * 경우에는 탐색 전과 동일한 미로를 출력)
			 * 
			 * 탈출 가능 한 경우 * 를 통해서 이동 경로를 표기한다
			 * 공백 일 경우 이동이 가능하며, # 기호는 이동이 불가능한 위치를 의미한다
			 */
			var oMap = new char[,] {
				{ '#', '#', '#', '#', 'S', '#', '#', '#', '#' },
				{ '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
				{ '#', '#', '#', ' ', ' ', '#', '#', '#', '#' },
				{ '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#' },
				{ '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
				{ '#', '#', ' ', '#', ' ', '#', ' ', ' ', '#' },
				{ '#', '#', 'E', '#', '#', '#', '#', '#', '#' }
			};

			Console.WriteLine("=====> 탐색 전 <=====");
			PrintMap(oMap);

			FindPath(oMap, 4, 0);

			Console.WriteLine("\n=====> 탐색 후 <=====");
			PrintMap(oMap);
#elif P02_11
			/*
			 * 과제 2 - 11
			 * - 수를 입력 받은 후 해당 수를 이미지로 출력하기
			 * 
			 * Ex)
			 * 숫자 입력 : 012
			 * 
			 * =====> 결과 <=====
			 * *****     * *****
			 * *   *     *     *
			 * *   *     *     *
			 * *   *     * *****
			 * *   *     * *
			 * *   *     * *
			 * *****     * *****
			 */
			Console.Write("숫자 입력 : ");
			string oStr = Console.ReadLine();

			Console.WriteLine("\n=====> 결과 <=====");
			PrintDigits(oStr);
#elif P02_12
			/*
			 * 과제 2 - 12
			 * - 개수를 입력 받아 해당 개수만큼 하노이 탑 시뮬레이션 결과 출력하기
			 * 
			 * Ex)
			 * 개수 입력 : 3
			 * 
			 * =====> 결과 <=====
			 * 1 번 원반 : 1 -> 3 이동
			 * 2 번 원반 : 1 -> 2 이동
			 * 1 번 원반 : 3 -> 1 이동
			 * 3 번 원반 : 1 -> 3 이동
			 * 1 번 원반 : 2 -> 1 이동
			 * 2 번 원반 : 2 -> 3 이동
			 * 1 번 원반 : 1 -> 3 이동
			 */
			Console.Write("개수 입력 : ");
			int nNumVals = int.Parse(Console.ReadLine());

			Console.WriteLine("\n=====> 결과 <=====");
			PrintHanoiTower(nNumVals);
#elif P02_13
			/*
			 * 과제 2 - 13
			 * - 사칙 연산 수식을 입력 받아 해당 결과 출력하기
			 * - 단, 문제를 단순화하기 위해서 수식의 피연산자는 1 자릿수 정수로 제한
			 * 
			 * PS.
			 * 후위 표기법 변환 방법 및 후위 표기법 연산 방법 참고
			 * 
			 * Ex)
			 * 수식 입력 : (3 + 5) * 2 - 1
			 * 결과 : 15
			 */
			Console.Write("수식 입력 : ");
			string oInfix = Console.ReadLine();

			string oPostfix = ConvertInToPostfix(oInfix);
			Console.WriteLine("결과 : {0}", GetCalcResult(oPostfix));
#endif // #elif P02_01
		}
	}
}
