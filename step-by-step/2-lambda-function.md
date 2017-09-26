# Build An Alexa Trivia Skill
[![Voice User Interface](https://m.media-amazon.com/images/G/01/mobile-apps/dex/alexa/alexa-skills-kit/tutorials/navigation/1-locked._TTH_.png)](1-voice-user-interface.md)[![Lambda Function](https://m.media-amazon.com/images/G/01/mobile-apps/dex/alexa/alexa-skills-kit/tutorials/navigation/2-on._TTH_.png)](2-lambda-function.md)[![Connect VUI to Code](https://m.media-amazon.com/images/G/01/mobile-apps/dex/alexa/alexa-skills-kit/tutorials/navigation/3-off._TTH_.png)](3-connect-vui-to-code.md)[![Testing](https://m.media-amazon.com/images/G/01/mobile-apps/dex/alexa/alexa-skills-kit/tutorials/navigation/4-off._TTH_.png)](4-testing.md)

## Setting Up A Lambda Function Using Amazon Web Services

In the [first step of this guide](https://github.com/alexa/skill-sample-nodejs-fact/blob/master/step-by-step/1-voice-user-interface.md), we built the Voice User Interface (VUI) for our Alexa skill. You can [read more about what a Lambda function is](http://aws.amazon.com/lambda), but for the purposes of this guide, what you need to know is that Lambda is where our code lives.  When a user asks Alexa to use our skill, it is our Lambda function that interprets the appropriate interaction, and provides the conversation back to the user.

1.  In **C#-setup-for-AWS-Lambda.md** (need ref) we describe in detail how to setup your C# development environment and how to build this C# sample project. Please follow that document and now build and upload the sample C# project to the AWS Lambda site. When you have done this **Go to http://aws.amazon.com and sign in to the console**.

2.  Next we must create a **Trigger** to enable the C# Lambda Function previously uploaded to be triggered from an Alexa Skill. Select the **Functions** menu option in the left panel and then select the Lambda function you have just uploaded from the Functions list, it should be at the top. Click the **Add trigger** button and select the **Alexa Skills Kit** option from the dropdown (tip: Clicking the outline square will display the dropdown). Click the **Next** button to create the **Trigger**.  

 ![](2-lambda-fig1.png)

 ![](2-lambda-fig2.png)

 ![](2-lambda-fig3.png)

 ![](2-lambda-fig4.png)

3. Finally, copy the ARN value from the top right corner of the screen, you will need this value in the next section of this guide.


<br/><br/>
<a href="3-connect-vui-to-code.md"><img src="https://m.media-amazon.com/images/G/01/mobile-apps/dex/alexa/alexa-skills-kit/tutorials/general/buttons/button_next_connect_vui_to_code._TTH_.png"/></a>

<img height="1" width="1" src="https://www.facebook.com/tr?id=1847448698846169&ev=PageView&noscript=1"/>
