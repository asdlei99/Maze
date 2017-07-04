//
//  MyTool.h
//  Unity-iPhone
//
//  Created by wjr on 2017/7/4.
//
//

#import <Foundation/Foundation.h>

@interface MyTool : NSObject

+ (NSString *)mutabledictionaryToJsonDic:(NSDictionary *)dic;
+(id)jsonToArrayOrNSDictionary:(NSString *)jsonString;
+ (void)sendUnityMessage:(NSString *)callbackName Mesage:(NSString *)msg;

@end
