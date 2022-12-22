//#define P01_01
//#define P01_02
//#define P01_03
//#define P01_04
//#define P01_05
#define P01_06

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// C# 사용
/*
 * 과제 1 - 1
 * - 점수를 입력 받아 해당 점수에 해당하는 학점 출력하기 (단, 1 자리 수에 따라 -, 0, + 출력)
 * 
 * -, 0, + 점수 범위
 * - 0 ~ 3 : -
 * - 4 ~ 6 : 0
 * - 7 ~ 9 : +
 * 
 * Ex)
 * 점수 입력 : 65
 * 결과 : D0
 * 
 * 
 * 과제 1 - 2
 * - 입력 된 정수의 합계와 평균 출력하기 (단, 0 이하의 점수가 입력 되면 입력 종료 및 결과 출력)
 * 
 * Ex)
 * 정수 1 입력 : 1
 * 정수 2 입력 : 3
 * 정수 3 입력 : 8
 * 정수 4 입력 : 0
 * 
 * =====> 결과 <=====
 * 합계 : 12
 * 평균 : 4
 * 
 * 
 * 과제 1 - 3
 * - 라인 수를 입력 받아 해당 라인 수 만큼 * 출력하기
 * 
 * Ex)
 * 라인 수 입력 : 5
 * 
 * =====> 결과 <=====
 *     *
 *    ***
 *   *****
 *  *******
 * *********
 */

// 주 언어 사용
/*
 * 과제 1 - 4
 * - 연습 문제 경험자 용 1 - 1 참고
 * 
 * 과제 1 - 5
 * - 연습 문제 경험자 용 1 - 2 참고
 * 
 * 과제 1 - 6
 * - 연습 문제 경험자 용 1 - 3 참고
 */
namespace Practice.Classes.Practice_01 {
	internal class CPractice_01 {
		/** 초기화 */
		public static void Start(string[] args) {
#if P01_01
			Console.Write("점수 입력 : ");
			int nScore = int.Parse(Console.ReadLine());

			Console.Write("결과 : ");

			if(nScore < 60) {
				Console.Write("F");
			} else {
				if(nScore >= 90) {
					Console.Write("A");
				} else if(nScore >= 80) {
					Console.Write("B");
				} else if(nScore >= 70) {
					Console.Write("C");
				} else {
					Console.Write("D");
				}

				if(nScore >= 100 || nScore % 10 >= 7) {
					Console.WriteLine("+");
				} else {
					Console.WriteLine((nScore % 10 <= 3) ? '-' : '0');
				}
			}
#elif P01_02
			int nVal = 0;
			int nNumVals = 0;

			var oVals = new int[5];

			do {
				Console.Write("정수 {0} 입력 : ", nNumVals + 1);
				nVal = int.Parse(Console.ReadLine());

				// 올바른 값 일 경우
				if(nVal > 0) {
					// 배열이 가득 찼을 경우
					if(nNumVals >= oVals.Length) {
						var oNewVals = new int[oVals.Length * 2];

						for(int i = 0; i < nNumVals; ++i) {
							oNewVals[i] = oVals[i];
						}

						oVals = oNewVals;
					}

					oVals[nNumVals++] = nVal;
				}
			} while(nVal > 0);

			int nSumVal = 0;

			for(int i = 0; i < nNumVals; ++i) {
				nSumVal += oVals[i];
			}

			Console.WriteLine("\n=====> 결과 <=====");
			Console.WriteLine("합계 : {0}", nSumVal);
			Console.WriteLine("평균 : {0}", nSumVal / (float)nNumVals);
#elif P01_03
			Console.Write("라인 수 입력 : ");
			int nNumLines = int.Parse(Console.ReadLine());

			int nMaxWidth = (nNumLines * 2) - 1;
			int nCenter = nMaxWidth / 2;

			for(int i = 0; i < nNumLines; ++i) {
				for(int j = 0; j < nMaxWidth; ++j) {
					Console.Write((j >= nCenter - i && j <= nCenter + i) ? '*' : ' ');
				}

				Console.WriteLine();
			}
#elif P01_04
			int nVal = 0;
			int nNumOddVals = 0;
			int nNumEvenVals = 0;

			var oVals = new int[5];

			do {
				Console.Write("정수 {0} 입력 : ", (nNumOddVals + nNumEvenVals) + 1);
				nVal = int.Parse(Console.ReadLine());

				// 올바른 값 일 경우
				if(nVal != 0) {
					// 배열이 가득 찼을 경우
					if(nNumOddVals + nNumEvenVals >= oVals.Length) {
						var oNewVals = new int[oVals.Length * 2];

						for(int i = 0; i < nNumOddVals; ++i) {
							oNewVals[i] = oVals[i];
						}

						for(int i = nNumEvenVals - 1; i >= 0 ; --i) {
							oNewVals[oNewVals.Length - (i + 1)] = oVals[oVals.Length - (i + 1)];
						}

						oVals = oNewVals;
					}

					// 홀수 일 경우
					if(nVal % 2 != 0) {
						oVals[nNumOddVals] = nVal; nNumOddVals += 1;
					} else {
						oVals[oVals.Length - (nNumEvenVals + 1)] = nVal; nNumEvenVals += 1;
					}
				}
			} while(nVal != 0);

			Console.WriteLine("\n=====> 결과 <=====");

			for(int i = 0; i < nNumOddVals; ++i) {
				Console.Write("{0}, ", oVals[i]);
			}

			for(int i = nNumEvenVals - 1; i >= 0; --i) {
				Console.Write("{0}, ", oVals[oVals.Length - (i + 1)]);
			}
#elif P01_05
			int nNumVals = 0;

			var oRandom = new Random((int)DateTime.Now.Ticks);
			var oAnswerVals = new int[4];

			while(nNumVals < oAnswerVals.Length) {
				int i = 0;
				int nVal = oRandom.Next(0, 9);

				for(i = 0; i < nNumVals && oAnswerVals[i] != nVal; ++i) {
					continue;
				}

				// 값이 없을 경우
				if(i >= nNumVals) {
					oAnswerVals[nNumVals++] = nVal;
				}
			}

			Console.Write("생성 한 숫자 : ");

			for(int i = 0; i < oAnswerVals.Length; ++i) {
				Console.Write("{0} ", oAnswerVals[i]);
			}

			Console.WriteLine();

			int nBallCount = 0;
			int nStrikeCount = 0;

			do {
				Console.Write("\n숫자 (4 개) 입력 : ");
				var oTokens = Console.ReadLine().Split();

				nBallCount = 0;
				nStrikeCount = 0;

				for(int i = 0; i < oTokens.Length; ++i) {
					int j = 0;
					int nVal = int.Parse(oTokens[i]);

					for(j = 0; j < oAnswerVals.Length && oAnswerVals[j] != nVal; ++j) {
						continue;
					}

					nBallCount += (i != j && j < oAnswerVals.Length) ? 1 : 0;
					nStrikeCount += (i == j && j < oAnswerVals.Length) ? 1 : 0;
				}

				Console.WriteLine("결과 : {0} Strike, {1} Ball", nStrikeCount, nBallCount);
			} while(nStrikeCount < oAnswerVals.Length);
#elif P01_06
			Console.Write("행, 열 개수 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int nNumRows = int.Parse(oTokens[0]);
			int nNumCols = int.Parse(oTokens[1]);

			int i = 0;
			int j = -1;

			int nVal = 0;
			int nDirection = 1;

			var oVals = new int[nNumRows, nNumCols];

			while(nVal < oVals.Length) {
				for(int k = 0; k < nNumCols; ++k) {
					j += nDirection;
					oVals[i, j] = nVal++;
				}

				nNumRows -= 1;

				for(int k = 0; k < nNumRows; ++k) {
					i += nDirection;
					oVals[i, j] = nVal++;
				}

				nNumCols -= 1;
				nDirection = -nDirection;
			}

			Console.WriteLine("\n=====> 결과 <=====");

			for(i = 0; i < oVals.GetLength(0); ++i) {
				for(j = 0; j < oVals.GetLength(1); ++j) {
					Console.Write("{0,4}", oVals[i, j]);
				}

				Console.WriteLine();
			}
#endif // #if P01_01
		}

#if P01_01

#elif P01_02

#elif P01_03

#elif P01_04

#elif P01_05

#elif P01_06
		
#endif // #if P01_01
	}
}
