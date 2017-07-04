//
//  MyInterface.m
//  Unity-iPhone
//
//  Created by diandianle on 15/8/7.
//
//

#import "UnityInterface.h"
#import <Foundation/Foundation.h>
#import <objc/runtime.h>
#import <objc/message.h>
#import "MyInterface.h"

#define SuppressPerformSelectorLeakWarning(Stuff) \
do { \
_Pragma("clang diagnostic push") \
_Pragma("clang diagnostic ignored \"-Warc-performSelector-leaks\"") \
Stuff; \
_Pragma("clang diagnostic pop") \
} while (0)

char* MakeStringCopy (const char* string) {
    if (string == NULL)
        return NULL;
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

@implementation UnityInterface

/*
 *  没有返回值的通用方法
 *
 *  @param p_method  方法的名称
 *  @param p_array 参数的数组
 *  @param p_length 参数个数的长度
 *
 */

#if defined(__cplusplus)
extern "C"
{
#endif

void _IOSGenarelClass(void *p_method, char **p_array, int p_length) {
    NSMutableArray* array = [[NSMutableArray alloc] init];
    
    NSString *methodname = [NSString stringWithUTF8String:p_method];

    for (int i = 0; i < p_length; i++) {
        [array addObject: [NSString stringWithCString: p_array[i] encoding:NSUTF8StringEncoding]];
    }
    MyInterface *platform = [MyInterface instance];
    SEL select = NSSelectorFromString([NSString stringWithFormat:@"%@:",methodname]);
    if ([platform respondsToSelector:select]) {
        [platform performSelector:select withObject:array];
    }
}
/*
 *  有返回值的通用方法
 *
 *  @param p_method  方法的名称
 *  @param p_array 参数的数组
 *  @param p_length 参数个数的长度
 *
 */

char* _IOSGenarelClassString(void *p_method, char **p_array, int p_length) {
    NSMutableArray* array = [[NSMutableArray alloc] init];
    
    NSString *methodname = [NSString stringWithUTF8String:p_method];
    
    for (int i = 0; i < p_length; i++) {
        [array addObject: [NSString stringWithCString: p_array[i] encoding:NSUTF8StringEncoding]];
    }
        
    NSString *result;
    MyInterface *platform = [MyInterface instance];
    SEL select = NSSelectorFromString([NSString stringWithFormat:@"%@:",methodname]);
    if ([platform respondsToSelector:select]) {
        result = [platform performSelector:select withObject:array];
    }
    return MakeStringCopy([result UTF8String]);
}
    
#if defined(__cplusplus)
}
#endif

@end
