//#define P03_01
//#define P03_02
//#define P03_03
//#define P03_04
#define P03_05

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._03010201000101_S_W_Engine_0001.E01.Practice.Classes.Practice__03
{
	internal class CPractice_03
	{
#if P03_01

#elif P03_02
		/** 결과를 반환한다 */
		private static int GetResult(int a_nMySel, int a_nComputerSel) {
			var oResults = new int[,] {
				{  0,  1, -1 },
				{ -1,  0,  1 },
				{  1, -1,  0 }
			};

			return oResults[a_nMySel - 1, a_nComputerSel - 1];
		}

		/** 선택 문자열을 반환한다 */
		private static string GetSelStr(int a_nSel) {
			var oSelStrs = new string[] {
				"바위", "가위", "보"
			};

			return oSelStrs[a_nSel - 1];
		}

		/** 결과 문자열을 반환한다 */
		private static string GetResultStr(int a_nResult) {
			var oResultStrs = new string[] {
				"졌습니다.", "비겼습니다.", "이겼습니다."
			};

			return oResultStrs[a_nResult + 1];
		}
#elif P03_03
		/** 단어를 반환한다 */
		private static string GetWord() {
			var oWords = new string[] {
				"Apple",
				"Samsung",
				"Microsoft"
			};

			var oRandom = new Random();
			return oWords[oRandom.Next(0, oWords.Length)];
		}

		/** 문자를 출력한다 */
		private static void PrintLetters(char[] a_oLetters) {
			for(int i = 0; i < a_oLetters.Length; ++i) {
				Console.Write("{0} ", a_oLetters[i]);
			}

			Console.WriteLine();
		}
#elif P03_04
		/** 잔돈 패턴을 반환한다 */
		private static int GetChangePatterns(int a_nTarget, int a_nIdx, List<(int, int)> a_oNumInfoDict) {
			// 진행이 불가능 할 경우
			if(a_nTarget <= 0 || a_nIdx >= a_oNumInfoDict.Count) {
				// 환전이 완료 되었을 경우
				if(a_nTarget == 0) {
					for(int i = 0; i < a_oNumInfoDict.Count; ++i) {
						Console.Write("{0} 원 ({1} 개), ", a_oNumInfoDict[i].Item1, a_oNumInfoDict[i].Item2);
					}

					Console.WriteLine();
				}

				return (a_nTarget == 0) ? 1 : 0;
			}

			int nNumPatterns = 0;

			for(int i = 0; i <= a_nTarget / a_oNumInfoDict[a_nIdx].Item1; ++i) {
				// 환전이 가능 할 경우
				if(a_oNumInfoDict[a_nIdx].Item2 + i <= 15) {
					var stPrevNumInfo = a_oNumInfoDict[a_nIdx];
					a_oNumInfoDict[a_nIdx] = (a_oNumInfoDict[a_nIdx].Item1, a_oNumInfoDict[a_nIdx].Item2 + i);

					nNumPatterns += GetChangePatterns(a_nTarget - (i * a_oNumInfoDict[a_nIdx].Item1), a_nIdx + 1, a_oNumInfoDict);
					a_oNumInfoDict[a_nIdx] = stPrevNumInfo;
				}
			}

			return nNumPatterns;
		}
#elif P03_05
		/** 맵을 출력한다 */
		private static void PrintMap(char[,] a_oMap)
		{
			for(int i = 0; i < a_oMap.GetLength(0); ++i)
			{
				for(int j = 0; j < a_oMap.GetLength(1); ++j)
				{
					Console.Write("{0}", a_oMap[i, j]);
				}

				Console.WriteLine();
			}
		}

		/** 모든 경로를 탐색한다 */
		private static void FindAllPaths(char[,] a_oMap, int a_nPosX, int a_nPosY, List<char[,]> a_oOutMapList)
		{
			// 배열을 벗어났을 경우
			if(a_nPosX < 0 || a_nPosX >= a_oMap.GetLength(1) || a_nPosY < 0 || a_nPosY >= a_oMap.GetLength(0))
			{
				return;
			}

			// 이동이 불가능 할 경우
			if(a_oMap[a_nPosY, a_nPosX] == '*' || a_oMap[a_nPosY, a_nPosX] == '#')
			{
				return;
			}

			// 목적지에 도착했을 경우
			if(a_oMap[a_nPosY, a_nPosX] == 'E')
			{
				a_oOutMapList.Add((char[,])a_oMap.Clone());
				return;
			}

			char chLetter = a_oMap[a_nPosY, a_nPosX];
			a_oMap[a_nPosY, a_nPosX] = (chLetter == 'S') ? 'S' : '*';

			var oXOffsets = new int[4] {
				1, -1, 0, 0
			};

			var oYOffsets = new int[4] {
				0, 0, 1, -1
			};

			for(int i = 0; i < oXOffsets.Length; ++i)
			{
				FindAllPaths(a_oMap, a_nPosX + oXOffsets[i], a_nPosY + oYOffsets[i], a_oOutMapList);
			}

			a_oMap[a_nPosY, a_nPosX] = chLetter;
		}
#endif // #if P03_02

		/** 초기화 */
		public static void Start(string[] args)
		{
#if P03_01
			/*
			 * 과제 3 - 1
			 * - 업/다운 게임 제작하기
			 * - 0 ~ 99 범위의 숫자 중 하나를 랜덤하게 생성 후 해당 숫자 맞추기
			 * 
			 * Ex)
			 * 정답 : 45
			 * 
			 * 숫자 입력 : 41
			 * 정답은 41 보다 높습니다.
			 * 
			 * 숫자 입력 : 58
			 * 정답은 58 보다 낮습니다.
			 * 
			 * 숫자 입력 : 45
			 * 정답니다.
			 */
			var oRandom = new Random();

			int nNum = 0;
			int nAnswer = oRandom.Next(0, 100);

			Console.WriteLine("정답 : {0}\n", nAnswer);

			do {
				Console.Write("숫자 입력 : ");
				nNum = int.Parse(Console.ReadLine());

				// 정답 일 경우
				if(nNum == nAnswer) {
					Console.WriteLine("정답입니다.");
				} else {
					Console.WriteLine("정답은 {0} 보다 {1}", nNum, (nNum < nAnswer) ? "높습니다." : "낮습니다.");
				}

				Console.WriteLine();
			} while(nNum != nAnswer);
#elif P03_02
			/*
			 * 과제 3 - 2
			 * - 바위/가위/보 게임 제작하기
			 * - 컴퓨터에게 이기면 게임을 계속 이어서 진행 (컴퓨터는 매 라운드마다 랜덤하게 선택)
			 * - 컴퓨터에게 졌을 경우 그 동안의 전적을 출력하고 게임 종료
			 * 
			 * Ex)
			 * 바위 (1), 가위 (2), 보 (3) 입력 : 1
			 * 이겼습니다. (나 - 바위, 컴퓨터 - 가위)
			 * 
			 * 바위 (1), 가위 (2), 보 (3) 입력 : 2
			 * 비겼습니다. (나 - 가위, 컴퓨터 - 가위)
			 * 
			 * 바위 (1), 가위 (2), 보 (3) 입력 : 3
			 * 졌습니다. (나 - 보, 컴퓨터 - 가위)
			 * 
			 * 전적 : 1 승 1 무 1 패
			 * 게임을 종료합니다.
			 */
			int nMySel = 0;
			int nComputeSel = 0;

			int nResult = 0;
			int nWinCount = 0;
			int nDrawCount = 0;

			var oRandom = new Random();

			do {
				Console.Write("바위 (1), 가위 (2), 보 (3) 입력 : ");

				nMySel = int.Parse(Console.ReadLine());
				nComputeSel = oRandom.Next(1, 4);

				nResult = GetResult(nMySel, nComputeSel);
				Console.WriteLine("{0} (나 - {1}, 컴퓨터 - {2})\n", GetResultStr(nResult), GetSelStr(nMySel), GetSelStr(nComputeSel));

				nWinCount += (nResult >= 1) ? 1 : 0;
				nDrawCount += (nResult == 0) ? 1 : 0;
			} while(nResult >= 0);

			Console.WriteLine("전적 : {0} 승 {1} 무 1 패", nWinCount, nDrawCount);
#elif P03_03
			/*
			 * 과제 3 - 3
			 * - 행맨 게임 제작하기
			 * - 랜덤하게 특정 단어를 선택 후 해당 단어 맞추기
			 * - 단, 알파벳 대/소문자는 구분하지 않는다.
			 * 
			 * Ex)
			 * 정답 : Apple
			 * 
			 * _ _ _ _ _
			 * 입력 : a
			 * 
			 * A _ _ _ _
			 * 입력 : e
			 * 
			 * A _ _ _ e
			 * 입력 : p
			 * 
			 * A p p _ e
			 * 입력 : l
			 * 
			 * A p p l e
			 * 게임을 종료합니다.
			 */
			string oAnswer = GetWord();
			var oLetters = new char[oAnswer.Length];

			for(int i = 0; i < oLetters.Length; ++i) {
				oLetters[i] = '_';
			}

			Console.WriteLine("정답 : {0}\n", oAnswer);

			do {
				PrintLetters(oLetters);

				Console.Write("입력 : ");
				char chLetter = char.Parse(Console.ReadLine());

				for(int i = 0; i < oAnswer.Length; ++i) {
					// 문자가 존재 할 경우
					if(char.ToUpper(oAnswer[i]) == char.ToUpper(chLetter)) {
						oLetters[i] = oAnswer[i];
					}
				}

				Console.WriteLine();
			} while(oLetters.Contains('_'));

			PrintLetters(oLetters);
#elif P03_04
			/*
			 * 과제 3 - 4
			 * - 1,000 원을 동전으로 환전 할 때 환전 가능한 모든 경우의 수 출력하기
			 * - 환전 가능한 동전의 종류는 10 원, 50 원, 100 원, 500 원 이다.
			 * - 단, 최대 동전 개수는 각 동전마다 15 개로 제한
			 * 
			 * Ex)
			 * 10 원 (0 개), 50 원 (0 개), 100 원 (0 개), 500 원 (2 개)
			 * 10 원 (0 개), 50 원 (0 개), 100 원 (5 개), 500 원 (1 개)
			 * 
			 * ... 이하 생략
			 * 
			 * 결과 : 53 가지
			 */
			List<(int, int)> oCoinInfoList = new List<(int, int)>() {
				(10, 0), (50, 0), (100, 0), (500, 0)
			};

			int nNumPatterns = GetChangePatterns(1000, 0, oCoinInfoList);
			Console.WriteLine("\n결과 : {0} 가지", nNumPatterns);
#elif P03_05
			/*
			 * 과제 3 - 6
			 * - 미로를 탈출 할 수 있는 모든 경로를 탐색하는 함수 제작하기 (단, 탈출하기 위한 미로는 동적으로 생성하기 않고 
			 * 미리 제작)
			 * 
			 * Ex)
			 * =====> 탐색 전 <=====
			 * ####S####
			 * #       #
			 * ###   ###
			 * #   #   #
			 * ##      #
			 * ## # #  #
			 * ##E######
			 * 
			 * Case 1. 탐색에 성공했을 경우
			 * =====> 탐색 후 <=====
			 * 경로 1.
			 * ####S####
			 * #   *   #
			 * ###** ###
			 * #  *#   #
			 * ##**    #
			 * ##*# #  #
			 * ##E######
			 *
			 * 경로 2.
			 * ####S####
			 * #   *   #
			 * ### **###
			 * #   #*  #
			 * ##****  #
			 * ##*# #  #
			 * ##E######
			 * 
			 * Case 2. 탐색에 실패했을 경우
			 * =====> 탐색 후 <=====
			 * ####S####
			 * #       #
			 * ###   ###
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

			var oMapList = new List<char[,]>();
			FindAllPaths(oMap, 4, 0, oMapList);

			Console.WriteLine("\n=====> 탐색 후 <=====");

			// 경로가 없을 경우
			if(oMapList.Count <= 0)
			{
				PrintMap(oMap);
			}
			else
			{
				for(int i = 0; i < oMapList.Count; ++i)
				{
					Console.WriteLine("경로 {0}.", i + 1);
					PrintMap(oMapList[i]);

					Console.WriteLine();
				}
			}
#endif // #if P03_01
		}
	}
}
