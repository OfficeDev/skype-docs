---
title: Installing the trusted root certificate
ms.prod: SKYPE
ms.assetid: c96be8c9-4498-4cfa-82c4-44e1e396ec3f
---


# Installing the trusted root certificate

 **Last modified:** January 28, 2016
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015
 
Installing a trusted root certificate is necessary only if you are notified that the certificate of authority is not trusted on any machine. This can occur when you use a private or custom certificate server instead of acquiring certificates from an established public certificate of authority.
  
    

## Installing a trusted root certificate


1. On the machine that requires a certificate, in your web browser, navigate to your local certification server. This should be the same certificate of authority used for generating the server and, optionally, client certificates.
    
  
2. Choose **Download a CA certificate**, **certificate chain**, or **CRL link**, as needed.
    
  
3. Select the appropriate certificate of authority from the list and choose the **Base 64 Encoding** method.
    
  
4. Choose the **Download CA certificate** link and then choose **Open** option when prompted to open or save the certificate.
    
  
5. When the certificate window opens, choose **Install Certificate…**. The Certificate Import wizard appears.
    
  
6. In the wizard, choose **Next**. Then, when you are prompted for the Certificate Store, choose **Place all certificates in the following store**. Select the Trusted Root Certification Authorities store.
    
  
7. Complete the remaining steps of the wizard and click **Finish**.
    
  
Upon completing the wizard, you next want to add the certificate snap-ins using the Microsoft Management Console (MMC).
  
    
    

## Adding certificate snap-ins


1. Launch MMC (mmc.exe).
    
  
2. Choose **File** > **Add/Remove Snap-ins**.
    
  
3. Choose **Certificates**, then choose **Add**.
    
  
4. Choose **My user account**.
    
  
5. Choose **Add** again and this time select **Computer Account**.
    
  
6. Move the new certificate from the **Certificates-Current User** > **Trusted Root Certification Authorities** into **Certificates (Local Computer)** > **Trusted Root Certification Authorities**.
    
  

## Additional resources
<a name="bk_addresources"> </a>


-  [Overview of Skype for Business SDN Interface](overview.md)
    
  
-  [Appendix to Skype for Business SDN Interface](appendix.md)
    
  

