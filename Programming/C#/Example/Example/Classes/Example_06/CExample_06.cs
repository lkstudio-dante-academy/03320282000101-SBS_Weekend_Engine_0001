#define E06_WHILE
#define E06_FOR
#define E06_DO_WHILE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Classes.Example_06 {
	class CExample_06 {
		/** 초기화 */
		public static void Start(string[] args) {
#if E06_WHILE

#elif E06_FOR

#elif E06_DO_WHILE

#endif // #if E06_WHILE
		}
	}
}
