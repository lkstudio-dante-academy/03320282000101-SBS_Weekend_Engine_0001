using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 데이터 표현 방법
 * - 컴퓨터는 0 과 1 로 이루어진 2 진수를 다루는 기계이기 때문에, 컴퓨터가 다루는 데이터의
 * 특징을 이해하기 위해서는 2 진수를 비롯한 10 진수와 16 진수를 알아두는 것이 좋다.
 * (즉, 2 진수는 컴퓨터에게 익숙한 숫자 체계이지만 사용자 (프로그래머) 는 대부분 10 진수가
 * 익수하기 때문에 해당 숫자 체계를 좀 더 수월하게 인식하기 위해서 16 진수가 사용 된다는
 * 것을 알 수 있다.)
 * 
 * 진수란?
 * - 특정 범위의 수를 기수로하는 숫자 체계를 의미한다. (즉, 2 진수는 0 과 1 을 기수로 하는
 * 숫자 체계를 의미한다.)
 * 
 * 따라서, 프로그래밍에서 자주 활용되는 진수는 2 진수, 10 진수, 16 진수가 있다.
 * 
 * 2 진수 범위
 * - 0, 1
 * 
 * 10 진수 범위
 * - 0, 1, 2, 3, 4, 5, 6, 7, 8, 9
 * 
 * 16 진수 범위
 * - 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, A, B, C, D, E, F
 * 
 * 컴퓨터 음수 표현 방법
 * - 컴퓨터는 음수를 표현하기 위해서 2 의 보수법을 사용한다. (즉, 단순히 최상위 비트를 0 
 * 또는 1 로 명시해서 음수를 표현하는 것이 아니라 2 의 보수법을 사용함으로써 비트 연산에 
 * 대한 처리를 단순화 시켰다는 것을 알 수 있다.)
 */
namespace Example._03320282000101_SBS_Weekend_Engine_0001.E01.Example.Classes.Example_03
{
	class CE01Example_03
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
			int nVal01 = 10;
			int nVal02 = 0x10;
			int nVal03 = 0b10;

			Console.WriteLine("{0}, {1}, {2}", nVal01, nVal02, nVal03);
		}
	}
}
