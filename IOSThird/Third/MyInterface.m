//
//  MyInterface.m
//  Unity-iPhone
//
//  Created by wjr on 2017/7/4.
//
//

#import "MyInterface.h"
#import "MyTool.h"

@implementation MyInterface

+ (MyInterface *)instance {
    static MyInterface *dataSource = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        dataSource = [[MyInterface alloc] init];
    });
    return dataSource;
}

//基础功能
- (void)SendPlatformUnityMessage:(NSArray *)arr {
    NSLog(@"wjr------SendPlatformUnityMessage:%@",arr);
    NSInteger flog = [[arr objectAtIndex:0] integerValue];
    
    switch (flog) {
        case 1:
            [MyTool sendUnityMessage:@"WechatLoginCallBack" Mesage:@"adssadasd"];
            break;
    }
}

@end
