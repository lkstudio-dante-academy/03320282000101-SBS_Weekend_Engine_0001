#define E11_DELEGATE_01
#define E11_DELEGATE_02
#define E11_DELEGATE_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 델리게이트란?
 * - 메서드를 간접적으로 호출해 줄 수 있는 기능을 의미한다. (즉, 델리게이트를 활용하면 메서드를 데이터처럼 취급하는 것이
 * 가능하다.)
 * 
 * 따라서, 델리게이트를 통해서 특정 메서드를 다른 메서드의 입력으로 전달하거나 특정 메서드가 반환 값으로 다른 메서드를 반환
 * 하는 것이 가능하다는 것을 알 수 있다. (즉, C/C++ 의 함수 포인터와 유사한 개념이다.)
 * 
 * C# 델리게이트 선언 방법
 * - 반환형 + delegate + 델리게이트 이름 + 입력 (매개 변수)
 * 
 * Ex)
 * void delegate SomeDelegateA(void);			<- 입력 X, 출력 X
 * void delegate SomeDelegateB(int a_nVal);		<- 입력 O, 출력 X
 * int delegate SomeDelegateC(volid);			<- 입력 X, 출력 O
 * int delegate SomeDelegateD(int a_nVal);		<- 입력 O, 출력 O
 */
namespace Example.Classes.Example_11 {
	internal class CExample_11 {
#if E11_DELEGATE_01

#elif E11_DELEGATE_02

#elif E11_DELEGATE_03

#endif // E11_DELEGATE_01

		/** 초기화 */
		public static void Start(string[] args) {
#if E11_DELEGATE_01

#elif E11_DELEGATE_02

#elif E11_DELEGATE_03

#endif // E11_DELEGATE_01
		}
	}
}
