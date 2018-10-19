---
title: Using Skype for Business Server 2015 Logging Tool
TOCTitle: Using Skype for Business Server 2015 Logging Tool
ms:assetid: 19906004-90ca-4287-bbe1-4c63983556f6
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466134(v=office.16)
ms:contentKeyID: 65240058
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Using Skype for Business Server 2015 Logging Tool


_**Applies to:** Skype for Business 2015_

An important part of the process of developing a UCMA 5.0 application is the ability to trace the protocol stack. To do this, you will enable tracing. By viewing the contents of the protocol stack during or after the execution of your code, you can familiarize yourself with the topology of UCMA 5.0.

## Setting up a protocol trace

When you install the UCMA 5.0 SDK, the Skype for Business Server 2015 Logging Tool (OCSLogger.exe) is located in the SDK\\Core\\Tracing folder in the UCMA 5.0 SDK installation directory. Launching OCSLogger.exe opens the window shown in the following figure. Before starting a trace, the user should do the following:

  - Select a trace provider.
    
    The default choices are **Collaboration**, **S4**, **SpeechComponent**, and **SpeechVxmlComponent**. You can start a trace for one or more of the listed components.

  - Specify a log file folder.
    
    When UCMA 5.0 SDK is installed it creates a Tracing folder under the %WinDir% directory. Skype for Business Server 2015 Logging Tool writes log files to this directory. If you want log files written to a different direction, click the **Browse** button and select or create the folder in which log files are written.

  - **Define the tracing level.**
    
    By clicking the appropriate tracing **Level** radio button, the user can filter the kinds of messages that will be recorded in the log file. Checking the **Errors** radio button causes only error messages to be recorded to the log file. Each radio button below the **Errors** button causes more messages to be recorded to the log file.

### Account permissions

To start a protocol trace you must be a member of the Administrators group on the computer running the protocol trace.

### Viewing a protocol trace

You can have a trace running for each of the two trace provider components.

Skype for Business Server 2015 Logging Tool produces .ETL files as its output. By default, these files are written to the %WINDIR%\\Tracing directory. The S4.etl file in this directory will contain error, warning, and protocol messages.

## Skype for Business Server Logging Tool user interface

Skype for Business Server 2015 Logging Tool displays a main dialog and four dialogs available through the **Advanced Options** menu item. These are discussed in detail in the following topics.

![Lync Server Logging Tool](images/Dn466134.OCSLoggingTool(Office.16).jpg "Lync Server Logging Tool")

### Main dialog

Skype for Business Server 2015 Logging Tool menu bar consists of the following menu items.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Menu Item</p></th>
<th><p>Action</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><strong>Start Logging</strong>/<strong>Stop Logging</strong></p></td>
<td><p>Starts logging the specified components, using the options chosen in the user interface. After logging starts, the <strong>Start Logging</strong> menu item is replaced by <strong>Stop Logging</strong>.</p></td>
</tr>
<tr class="even">
<td><p><strong>View Log Files</strong></p></td>
<td><p>Opens the <strong>View Log Files</strong> dialog box. Click <strong>View</strong> to convert the log files of the checked components to text and display them.</p></td>
</tr>
<tr class="odd">
<td><p><strong>Analyze Log Files</strong></p></td>
<td><p>Opens the <strong>Analyze Log Files</strong> dialog box. Click <strong>Analyze</strong> to analyze the log files of the checked components using Snooper.exe.</p></td>
</tr>
<tr class="even">
<td><p><strong>Advanced Options</strong></p></td>
<td><p>Opens the <strong>Advanced Options</strong> dialog box, which displays options for formatting, buffering, clock resolution, and additional components. These options are described in detail in the following sections.</p></td>
</tr>
<tr class="odd">
<td><p><strong>Exit</strong></p></td>
<td><p>Exits Skype for Business Server 2015 Logging Tool.</p></td>
</tr>
<tr class="even">
<td><p><strong>Help</strong></p></td>
<td><p>Displays Help for Skype for Business Server 2015 Logging Tool.</p></td>
</tr>
</tbody>
</table>


The remainder of the main dialog consists of panes for **Logging Options** and **Global Options**, as well as two text boxes that specify the paths to the log file folder and format file folder.

#### Logging options

The **Components** pane displays four components: **Collaboration**, **S4**, **SpeechComponent**, and **SpeechVxmlComponent**. Check one or more to enable logging the selected components.

The **Level** pane allows you to specify the types of messages that are recorded in the log file. The levels are arranged from most restrictive (**Fatal Errors**) at the top to least restrictive (**All**) at the bottom. The levels are cumulative, with each level including messages of that type, in addition to messages from all previous levels (that is, levels higher in the list of radio buttons). For example, clicking the **Warnings** level causes all warning messages to be logged, in addition to all error messages and fatal error messages.

The **Flags** pane enables you to set the types of messages that are logged. Any combination of the flags shown in the following table can be chosen.


> [!NOTE]
> <P>Some flags are not available for all components. The following table describes the flags and lists any component limitations.</P>



<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Flag</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><strong>TF_COMPONENT</strong></p></td>
<td><p>Log miscellaneous messages not covered by the other trace flag categories.</p></td>
</tr>
<tr class="even">
<td><p><strong>TF_PROTOCOL</strong></p></td>
<td><p>Log protocol messages or data—SIP or CCCP messages. Other components can also log their text-based messages.</p>
<p>Not available for the <strong>SpeechComponent</strong> and <strong>SpeechVxmlComponent</strong> components.</p></td>
</tr>
<tr class="odd">
<td><p><strong>TF_CONNECTION</strong></p></td>
<td><p>Log connection-related errors or warnings. Information on connections established to and from a particular component, etc. This can also include significant network-level information for components that do not have the concept of a &quot;connection.&quot;</p>
<p></p>
<p>Not available for the <strong>S4</strong>, <strong>SpeechComponent</strong>, and <strong>SpeechVxmlComponent</strong> components.</p></td>
</tr>
<tr class="even">
<td><p><strong>TF_DIAG</strong></p></td>
<td><p>Log diagnostics events that can be used to diagnose or troubleshoot the component. For example, for SipStack, these include certificate failures or errors, or DNS warnings or errors.</p>
<p></p>
<p>Not available for the <strong>S4</strong> and <strong>SpeechVxmlComponent</strong> components.</p></td>
</tr>
<tr class="odd">
<td><p><strong>All Flags</strong></p></td>
<td><p>Log messages in all categories.</p></td>
</tr>
</tbody>
</table>


#### Global options

The **Log File Options** pane contains the **Type** pane and the **Maximum Size** text box, which determine how new log records are inserted into the log file and its size. Choose **Circular** to indicate that when a log file reaches its maximum size, new records are to be placed at the beginning. Choose **Sequential** to indicate that new records should be placed at the end of the log file. Choose **New File** to start a new log file.

The **Real Time Options** pane contains a check box for **Enabled**. If **Enabled** is checked, a check box for **Display only** can be selected.

The **Filter Options** pane enables a user to restrict logging to specified URIs or fully-qualified domain names (FQDN), each of which requires input validation. When filtering is used, the log files will contain information only from the URIs or FQDNs listed in the **URI** and **FQDN** text boxes. Filtering can be applied to two user URIs, or two machine FQDNs (or IP addresses), or one URI and one FQDN. Click **Clear Filters** to clear the contents of the **URI** and **FQDN** text boxes.

The **Log File Folder** text box specifies the folder in which to write log files. By default this is %WinDir%\\Tracing\\.

### Advanced Options dialog—Formatting tab

The **Formatting** tab of the **Advanced Options** dialog box enables the user to determine the information that appears in log records, and how the information is formatted.

The **Log File Prefix** text box is used for formatting events that are written to log files. The **Real Time Prefix** text box is used for formatting events to the real-time tracing window.

The **Display times in UTC** check box determines whether times in log records are displayed in Coordinated Universal Time (UTC) (checked), or system time (unchecked).

The **Format file search path** text box specifies a semicolon-separated list of folder paths. OCSLogger searches for .TMF files in these paths if it is unable to find format information for a log record in the default.tmx file that is included with OCSLogger. This functionality is used primarily to support patching that results in new format information for some components, where that information is not present in default.tmx.

![Lync Server Logging Tool - Formatting](images/Dn466134.OCSLogger_Options_Fmt(Office.16).jpg "Lync Server Logging Tool - Formatting")

The variables used in log file formatting are shown in the following table.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Variable</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>%!COMP!%</p></td>
<td><p>The component name.</p></td>
</tr>
<tr class="even">
<td><p>%!LEVEL!%</p></td>
<td><p>The level name.</p></td>
</tr>
<tr class="odd">
<td><p>%!FLAGS!%</p></td>
<td><p>The flag name.</p></td>
</tr>
<tr class="even">
<td><p>%!FUNC!%</p></td>
<td><p>The function or method that generated the log record.</p></td>
</tr>
<tr class="odd">
<td><p>%1</p></td>
<td><p>Not used.</p></td>
</tr>
<tr class="even">
<td><p>%2</p></td>
<td><p>The source file and line number that generated the log record.</p></td>
</tr>
<tr class="odd">
<td><p>%3</p></td>
<td><p>The ID of the thread that generated the log record.</p></td>
</tr>
<tr class="even">
<td><p>%4</p></td>
<td><p>The time when the log record was generated.</p></td>
</tr>
<tr class="odd">
<td><p>%5</p></td>
<td><p>The cumulative processor time.</p></td>
</tr>
<tr class="even">
<td><p>%6</p></td>
<td><p>Not used.</p></td>
</tr>
<tr class="odd">
<td><p>%7</p></td>
<td><p>A local sequence number for the component.</p></td>
</tr>
<tr class="even">
<td><p>%8</p></td>
<td><p>The ID of the process that generated the log record.</p></td>
</tr>
<tr class="odd">
<td><p>%9</p></td>
<td><p>The number of the CPU that generated the log record.</p></td>
</tr>
</tbody>
</table>


The numeric variables (%n) can be followed by a printf-style format specifier. For example, %3\!04X\! displays the thread ID as a four-digit hexadecimal number, %7\!08X\! displays the thread ID as an eight-digit hexadecimal number, and %9\!D\! displays the CPU number as an integer. The %2, %4, and %5 variables represent string quantities, so if a format specifier is used, it must be one pertaining to strings, such as \!s\!.

After setting logging prefixes and the time to use in this dialog box, click **OK** to save your changes, or **Cancel** to discard them. If you click **Apply**, you can open another **Advanced Option** page on which to make changes. Clicking **Cancel** undoes any changes you have made since you clicked **Apply**.

### Advanced Options dialog—Buffering tab

The **Buffering** tab of the **Advanced Options** dialog box enables the user to set the buffer size, minimum and maximum number of buffers, and the **Real time display flush timer**, which specifies how often to flush the buffer in real-time logging. The default values are 64 KB for the buffer size, 20 and 40, respectively, for the minimum and maximum numbers of buffers, and 10 seconds for **Real Time Flush timer**.

![Lync Server Logging Tool - Buffering](images/Dn466134.OCSLogger_Options_Buf(Office.16).jpg "Lync Server Logging Tool - Buffering")

After setting the buffer size and the minimum and maximum numbers of buffers, and the timer, click **OK** to save your changes, or **Cancel** to discard them. If you click **Apply**, you can open another **Advanced Option** page on which to make changes. Clicking **Cancel** undoes any changes you have made since you clicked **Apply**.

### Advanced Options dialog—Clock Resolution tab

The **Clock Resolution** tab of the **Advanced Options** dialog box enables the user to specify the resolution of time stamps in log records. The choices are 10 milliseconds (the lowest resolution), 100 nanoseconds, or CPU frequency (the highest resolution).

![Lync Server Logging Tool - Clock Resolution](images/Dn466134.OCSLogger_Options_Clk(Office.16).jpg "Lync Server Logging Tool - Clock Resolution")

After setting the clock resolution in this dialog box, click **OK** to save your changes, or **Cancel** to discard them. If you click **Apply**, you can open another **Advanced Option** page on which to make changes. Clicking **Cancel** undoes any changes you have made since you clicked **Apply**.

### Advanced Options dialog—Additional Components tab

The **Additional Components** tab of the **Advanced Options** dialog box enables the user to specify additional components on which to log events. Enter the path for the component in the upper text box. Enter the name of the executable in the lower text box. Multiple paths and multiple executable files can be entered.

![Lync Server Logging Tool - Additional Components](images/Dn466134.OCSLogger_Options_Addl(Office.16).jpg "Lync Server Logging Tool - Additional Components")

After entering the paths and executable files in this dialog box, click **OK** to save your changes, or **Cancel** to discard them. If you click **Apply**, you can open another **Advanced Option** page on which to make changes. Clicking **Cancel** undoes any changes you have made since you clicked **Apply**.

