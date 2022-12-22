#define T02_01
#define T02_02
#define T02_03

#define T02_STARTER
#define T02_EXPERTER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 입문
/*
 */

// 경험자
/*
 */
namespace Training.Classes.Training_02 {
	internal class CTraining_02 {
		/** 초기화 */
		public static void Start(string[] args) {
#if T02_STARTER
#if T02_01

#elif T02_02

#elif T02_03

#endif // #if T02_01
#elif T02_EXPERTER
#if T02_01

#elif T02_02

#elif T02_03

#endif // #if T02_01
#endif // T02_STARTER
		}

#if T02_STARTER
#if T02_01

#elif T02_02

#elif T02_03

#endif // #if T02_01
#elif T02_EXPERTER
#if T02_01

#elif T02_02

#elif T02_03

#endif // #if T02_01
#endif // T02_STARTER
	}
}
