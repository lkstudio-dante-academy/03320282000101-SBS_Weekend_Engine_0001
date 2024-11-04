#define P01_01
#define P01_02
#define P01_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._03020203000101_SBS_Weekend_Engine_0001.E01.Practice.Classes.Practice_01
{
	internal class CP01Practice_01
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if P01_01
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
			 */
			Console.Write("점수 입력 : ");
			int nScore = int.Parse(Console.ReadLine());

			Console.Write("결과 : ");

			if(nScore < 60)
			{
				Console.Write("F");
			}
			else
			{
				if(nScore >= 90)
				{
					Console.Write("A");
				}
				else if(nScore >= 80)
				{
					Console.Write("B");
				}
				else if(nScore >= 70)
				{
					Console.Write("C");
				}
				else
				{
					Console.Write("D");
				}

				if(nScore >= 100 || nScore % 10 >= 7)
				{
					Console.WriteLine("+");
				}
				else
				{
					Console.WriteLine((nScore % 10 <= 3) ? '-' : '0');
				}
			}
#elif P01_02
			/*
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
			 */
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
			/*
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
#endif // #if P01_01
		}
	}
}
