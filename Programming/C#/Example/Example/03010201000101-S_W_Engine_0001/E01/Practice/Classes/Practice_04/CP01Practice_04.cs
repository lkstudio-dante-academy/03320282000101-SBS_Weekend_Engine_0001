#define P04_01
#define P04_02
#define P04_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._03010201000101_S_W_Engine_0001.E01.Practice.Classes.Practice_04
{
	internal class CP01Practice_04
	{
#if P04_01
		/** 비트를 교환한다 */
		private static void SwapBit(ref int a_nVal, int a_nIdxA, int a_nIdxB)
		{
			// 인덱스 A 가 클 경우
			if(a_nIdxA > a_nIdxB)
			{
				int nTemp = a_nIdxA;
				a_nIdxA = a_nIdxB;
				a_nIdxB = nTemp;
			}

			int nBitA = a_nVal & (1 << a_nIdxA);
			int nBitB = a_nVal & (1 << a_nIdxB);

			int nVal = a_nVal & ~(nBitA | nBitB);

			nBitA <<= a_nIdxB - a_nIdxA;
			nBitB >>= a_nIdxB - a_nIdxA;

			a_nVal = nVal | (nBitA | nBitB);
		}

		/** 비트를 출력한다 */
		private static void PrintBits(int a_nVal)
		{
			for(int i = (sizeof(int) * 8) - 1; i >= 0; --i)
			{
				int nMask = 1 << i;
				Console.Write("{0}{1}", ((a_nVal & nMask) != 0) ? 1 : 0, (i % 8 <= 0) ? " " : "");
			}
		}
#elif P04_02
		/** 값을 치환한다 */
		private static void ReplaceVals(int[] a_oVals, int a_nIdx) {
			// 치환이 불가능 할 경우
			if(a_nIdx < 0 || a_nIdx >= a_oVals.Length || a_oVals[a_nIdx] < 0 || a_oVals[a_nIdx] >= 10) {
				return;
			}

			a_oVals[a_nIdx] = -1;

			ReplaceVals(a_oVals, a_nIdx - 1);
			ReplaceVals(a_oVals, a_nIdx + 1);
		}
#elif P04_03
		/** 값 개수를 반환한다 */
		private static int[] GetNumVals(int[] a_oVals) {
			var oNumVals = new int[10];

			for(int i = 0; i < a_oVals.Length; ++i) {
				oNumVals[a_oVals[i]] += 1;
			}

			return oNumVals;
		}
#endif // #elif P04_01

		/** 초기화 */
		public static void Start(string[] args)
		{
#if P04_01
			/*
			 * 과제 4 - 1
			 * - 정수를 입력받아 해당 정수의 비트를 교환한다
			 * - 비트의 위치는 가장 오른쪽 비트부터 왼쪽 순으로 차례대로 0, 1, 2, ... 순으로 증가한다
			 * - 단, 비트를 교환하는 과정에서 다른 자료형 사용 불가 (즉, 정수만 사용 가능)
			 * 
			 * Ex)
			 * 정수 입력 : 15
			 * 
			 * =====> 교환 전 <=====
			 * 00000000 00000000 00000000 00001111
			 * 
			 * 비트 교환 위치 입력 : 0 8
			 * 
			 * =====> 교환 후 <=====
			 * 00000000 00000000 00000001 00001110
			 */
			Console.Write("정수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nVal);

			Console.WriteLine("\n=====> 교환 전 <=====");
			PrintBits(nVal);

			Console.Write("\n\n비트 교환 위치 입력 : ");
			var oTokens = Console.ReadLine().Split();

			SwapBit(ref nVal, int.Parse(oTokens[0]), int.Parse(oTokens[1]));

			Console.WriteLine("\n=====> 교환 전 <=====");
			PrintBits(nVal);
#elif P04_02
			/*
			 * 과제 4 - 2
			 * - 길이가 10 인 배열을 선언 후 0 ~ 13 사이의 값으로 랜덤하게 초기화한다
			 * - 인덱스 번호를 입력 받은 후 해당 인덱스 번호에 해당하는 요소가 1 자릿수 일 경우 -1 로 치환한다
			 * - 치환 과정은 인접한 요소가 2 자릿수가 아닐 때까지 반복한다
			 * 
			 * Ex)
			 * =====> 배열 요소 - 치환 전 <=====
			 * 0, 2, 10, 1, 3, 5, 8, 15, 10, 1
			 * 
			 * 치환 시작 위치 입력 : 4
			 * 
			 * =====> 배열 요소 - 치환 후 <=====
			 * 0, 2, 10, -1, -1, -1, -1, 15, 10, 1
			 */
			var oVals = new int[10];
			var oRandom = new Random();

			for(int i = 0; i < oVals.Length; ++i) {
				oVals[i] = oRandom.Next(0, 14);
			}

			Console.WriteLine("=====> 배열 요소 - 치환 전 <=====");

			for(int i = 0; i < oVals.Length; ++i) {
				Console.Write("{0}, ", oVals[i]);
			}

			Console.Write("\n\n치환 시작 위치 입력 : ");
			int.TryParse(Console.ReadLine(), out int nIdx);

			ReplaceVals(oVals, nIdx);
			Console.WriteLine("\n=====> 배열 요소 - 치환 후 <=====");

			for(int i = 0; i < oVals.Length; ++i) {
				Console.Write("{0}, ", oVals[i]);
			}
#elif P04_03
			/*
			 * 과제 4 - 3
			 * - 길이가 100 인 배열을 선언 후 0 ~ 9 사이의 값으로 랜덤하게 초기화한다
			 * - 각 숫자의 개수를 출력한다
			 * 
			 * Ex)
			 * =====> 배열 요소 <=====
			 * 9, 1, 2, 0, 9, 0, 7, 5, 9, 3
			 * 
			 * =====> 결과 <=====
			 * [0] : 2 개
			 * [1] : 1 개
			 * [2] : 1 개
			 * 
			 * ... 이하 생략
			 */
			var oVals = new int[100];
			var oRandom = new Random();

			for(int i = 0; i < oVals.Length; ++i) {
				oVals[i] = oRandom.Next(0, 10);
			}

			Console.WriteLine("=====> 배열 요소 <=====");

			for(int i = 0; i < oVals.Length; ++i) {
				Console.Write("{0}{1}", oVals[i], ((i + 1) % 10 <= 0) ? "\n" : ", ");
			}

			var oNumVals = GetNumVals(oVals);

			Console.WriteLine("\n\n=====> 결과 <=====");

			for(int i = 0; i < oNumVals.Length; ++i) {
				Console.WriteLine("[{0}] : {1} 개", i, oNumVals[i]);
			}
#endif // #elif P04_01
		}
	}
}
