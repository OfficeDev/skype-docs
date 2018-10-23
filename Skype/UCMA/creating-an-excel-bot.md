---
title: Creating an Excel bot
TOCTitle: Creating an Excel bot
ms:assetid: 10649396-2ea8-43c9-9dc6-bfcb77d6801d
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454841(v=office.16)
ms:contentKeyID: 65240112
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Creating an Excel bot

Build a Bot can be used to create bots that require no programming at all. To accomplish this, follow the steps in the following procedure.

1. Create the following folders in your test and target machines:
    
   - For 64-bit (x64), C:\\Windows\\SysWOW64\\config\\systemprofile\\Desktop   
   - For 32-bit (x86), C:\\Windows\\System32\\config\\systemprofile\\Desktop

2. Copy the contents of the Build a Bot release to a location in your target machine.

3. Open the BotConfig.xlsx spreadsheet. In the first tab of the spreadsheet, called botInfo, enter the information related to the application account under which your bot will run, as shown in the following illustration.
    
   **Account information for the BuildABot application**
      
   ![BuildABot application account information](images/Dn454841.BuildABotExcel1(Office.16).png "BuildABot application account information")

4. In the static spreadsheet tab, enter in the first column the questions the bot will be able to answer. Their respective answers should come in the second column. The bot will match any user question (ignoring the case) to the first column, and then use the answer in the second column. **Do not enter question marks for the questions** (first column), as question marks are automatically handled by the ExcelBot engine.
    
   **BuildABot static data**
    
   ![BuildABot static data](images/Dn454841.BuildABotExcel2(Office.16).png "BuildABot static data")

5. You can add more tabs to your spreadsheet, parameterizing their name with the literal text \<term\> (for example: what is \<term\>). The bot will then match the term passed by the user to the first column of the tab, and will use the corresponding text in the second column to answer. The answer to the "default" question cell (as illustrated below) will be used by the bot if none of the other questions matches the user input.
    
   **BuildABot term glossary**
    
   ![BuildABot term glossary](images/Dn454841.BuildABotExcel3(Office.16).png "BuildABot term glossary")

