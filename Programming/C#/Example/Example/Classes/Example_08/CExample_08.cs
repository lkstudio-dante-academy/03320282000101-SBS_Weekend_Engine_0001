#define E08_METHOD_01
#define E08_METHOD_02
#define E08_METHOD_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 메서드 (함수) 란?
 * - 프로그램 명령문의 특정 부분을 그룹화시켜서 필요 할 때 재활용 할 수 있는 기능을 의미한다.
 * (즉, 메서드를 활용하면 중복되는 명령문을 최소화 시키는 것이 가능하다는 것을 알 수 있다.)
 * 
 * C# 메서드 선언 방법
 * - 반환형 + 메서드 이름 + 매개 변수
 * 
 * C# 메서드 구현 방법
 * - 반환형 + 메서드 이름 + 매개 변수 + 메서드 몸체
 * 
 * Ex)
 * // 선언
 * abstract void SomeMethod01(int a_nVal);
 * 
 * // 구현
 * void SomeMethod02(int a_nVal) {
 *		// Do Something
 * }
 * 
 * C# 메서드 유형
 * - 입력 O, 출력 O		<- int SomeMethod01(int a_nVal);
 * - 입력 O, 출력 X		<- void SomeMethod02(int a_nVal01, int a_nVal02);
 * - 입력 X, 출력 O		<- int SomeMethod03();
 * - 입력 X, 출력 X		<- void SomeMethod04();
 * 
 * 메서드 입력 vs 출력
 * - 메서드의 입력 데이터의 개수는 필요한만큼 명시하는 것이 가능하지만 출력 데이터는 하나만
 * 존재하는 차이점이 있다. (즉, 특정 메서드가 여러 데이터를 결과로 출력하기 위해서는 컬렉션 등을
 * 활용 할 필요가 있다는 것을 알 수 있다.)
 */
namespace Example.Classes.Example_08 {
	internal class CExample_08 {
		/** 초기화 */
		public static void Start(string[] args) {
#if E08_METHOD_01
			var oRandom = new Random();
			List<int> oValList = new List<int>();

			for(int i = 0; i < 10; ++i) {
				oValList.Add(oRandom.Next(0, 100));
			}

			Console.WriteLine("=====> 리스트 요소 <=====");
			PrintVals(oValList);

			var stResult = GetMinMaxVal(oValList);

			Console.WriteLine("\n최소 : {0}", stResult.Item1);
			Console.WriteLine("최대 : {0}", stResult.Item2);
#elif E08_METHOD_02

#elif E08_METHOD_03

#endif // #if E08_METHOD_01
		}

#if E08_METHOD_01
		/** 값을 출력한다 */
		static void PrintVals(List<int> a_oValList) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				Console.Write("{0}, ", a_oValList[i]);
			}

			Console.WriteLine("\n");
		}

		/** 최소, 최대 값을 반환한다 */
		static (int, int) GetMinMaxVal(List<int> a_oValList) {
			int nMinVal = int.MaxValue;
			int nMaxVal = int.MinValue;

			for(int i = 0; i < a_oValList.Count; ++i) {
				nMinVal = (nMinVal < a_oValList[i]) ? nMinVal : a_oValList[i];
				nMaxVal = (nMaxVal > a_oValList[i]) ? nMaxVal : a_oValList[i];
			}

			return (nMinVal, nMaxVal);
		}
#elif E08_METHOD_02

#elif E08_METHOD_03

#endif // #if E08_METHOD_01
	}
}
