//
//  MyTool.m
//  Unity-iPhone
//
//  Created by wjr on 2017/7/4.
//
//

#import "MyTool.h"

const NSString *unityscene = @"PT_ThirdNoticer";

@implementation MyTool

/*
 *   字典转化json
 */
+ (NSString *)mutabledictionaryToJsonDic:(NSDictionary *)dic {
    NSError *jsonError = nil;
    NSData *jsonData = nil;
    jsonData=[NSJSONSerialization dataWithJSONObject:dic options:NSJSONWritingPrettyPrinted error:&jsonError];
    if([jsonData length] > 0 && jsonError == nil) {
        NSString *jsonString = [[NSString alloc]initWithData:jsonData encoding:NSUTF8StringEncoding];
        return jsonString;
    }
    return nil;
}

/*
 *   将JSON串转化为字典或者数组
 */
+(id)jsonToArrayOrNSDictionary:(NSString *)jsonString {
    NSData* jsonData = [jsonString dataUsingEncoding:NSASCIIStringEncoding];
    NSError *error = nil;
    id jsonObject = [NSJSONSerialization JSONObjectWithData:jsonData
                                                    options:NSJSONReadingAllowFragments
                                                      error:&error];
    
    if (jsonObject != nil && error == nil){
        return jsonObject;
    }
    else{
        // 解析错误
        return nil;
    }
}

+ (void)sendUnityMessage:(NSString *)callbackName Mesage:(NSString *)msg {
    
    NSParameterAssert(callbackName);
    
    NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:callbackName ,@"callbackName" ,[NSString stringWithFormat:@"%@", msg] ,@"messageContent", nil];
    
    const char *ptr = [[MyTool mutabledictionaryToJsonDic:dic] cStringUsingEncoding:NSUTF8StringEncoding];
    
    const char *scene = [unityscene cStringUsingEncoding:NSUTF8StringEncoding];
    
    UnitySendMessage(scene, "OnGetMessageSuc",ptr);
}

@end
