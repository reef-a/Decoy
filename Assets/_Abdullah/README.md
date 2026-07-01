# Bank Scam Detection Prototype - Setup Guide

## Overview
This prototype is a VR/Mobile/PC game where players learn to identify bank scams through email, SMS, and call simulations. The player is in an office setting and can only rotate the camera to look at a PC monitor (for emails) or a phone (for SMS/calls).

## Scripts Created

### Core Scripts
1. **CameraRotationController.cs** - Handles camera rotation (no movement) for PC, Mobile, and VR
2. **ObjectInteraction.cs** - Detects when player looks at interactable objects using raycasting
3. **ScamMessageData.cs** - Data structures for messages and ScriptableObject for message database

### Simulation Scripts
4. **EmailSimulation.cs** - Handles email display and scam detection on PC monitor
5. **PhoneSimulation.cs** - Handles SMS and call simulations on phone
6. **SampleMessageDatabase.cs** - Contains sample bank scam/legitimate messages

## Setup Instructions

### 1. Create Message Database Asset
1. Right-click in Project window → Create → Abdullah → Message Database
2. Name it "BankScamMessages"
3. In the inspector, populate the Messages array with the sample data from SampleMessageDatabase.cs
4. The sample messages include:
   - 4 Email examples (2 scams, 2 legitimate)
   - 3 SMS examples (2 scams, 1 legitimate)
   - 3 Call examples (2 scams, 1 legitimate)

### 2. Scene Setup
1. Open or create your office scene
2. Add a **Player GameObject** with:
   - Camera component
   - CameraRotationController script attached
3. Add **PC Monitor GameObject** with:
   - Collider (Box Collider)
   - Layer set to "Interactable" (create this layer)
   - EmailSimulation script attached
   - Assign the MessageDatabase asset
4. Add **Phone GameObject** with:
   - Collider (Box Collider)
   - Layer set to "Interactable"
   - PhoneSimulation script attached
   - Assign the MessageDatabase asset
5. Add **ObjectInteraction** script to the player or a manager GameObject:
   - Assign the EmailSimulation reference
   - Assign the PhoneSimulation reference
   - Set Interactable Layer to your "Interactable" layer
   - Set Interaction Distance (default 3f)

### 3. UI Setup (Canvas)

#### For Email Monitor:
Create a Canvas as a child of the PC Monitor with:
- Panel (emailPanel)
  - TextMeshPro - Sender (senderText)
  - TextMeshPro - Subject (subjectText)
  - TextMeshPro - Content (contentText)
  - Button - "Scam" (scamButton)
  - Button - "Legitimate" (legitimateButton)
  - Button - "Next" (nextButton)
  - TextMeshPro - Feedback (feedbackText)
  - TextMeshPro - Explanation (explanationText)

#### For Phone:
Create a Canvas as a child of the Phone with:
- Panel (phonePanel)
  - TextMeshPro - Sender (senderText)
  - TextMeshPro - Message (messageText)
  - TextMeshPro - Call Status (callStatusText)
  - Button - "Answer Call" (answerCallButton)
  - Button - "Scam" (scamButton)
  - Button - "Legitimate" (legitimateButton)
  - Button - "Next" (nextButton)
  - TextMeshPro - Feedback (feedbackText)
  - TextMeshPro - Explanation (explanationText)

### 4. Layer Setup
1. Go to Edit → Project Settings → Tags and Layers
2. Add a new User Layer named "Interactable"
3. Set your PC Monitor and Phone GameObjects to this layer
4. In ObjectInteraction script, set the Interactable Layer mask to this layer

### 5. Platform-Specific Setup

#### VR Support:
- Install XR Plugin Management through Package Manager
- Configure your VR platform (Oculus, OpenXR, etc.)
- The camera rotation will work automatically with VR controllers

#### Mobile Support:
- The touch controls are built into CameraRotationController
- Ensure your canvas is set to Scale with Screen Size

#### PC Support:
- Mouse controls are built into CameraRotationController
- Click and drag to rotate camera

## How It Works

1. **Player Rotation**: Player can only rotate camera using mouse (PC), touch (mobile), or VR controllers
2. **Object Detection**: When player looks at PC monitor or phone (within interaction distance), the UI appears
3. **Message Display**: 
   - PC shows emails with sender, subject, and content
   - Phone shows SMS messages directly, or incoming call that must be answered first
4. **Decision Making**: Player clicks "Scam" or "Legitimate" button
5. **Feedback**: System shows if the decision was correct and explains why
6. **Progression**: Player clicks "Next" to move to the next message

## Adding Custom Messages

To add your own scam/legitimate messages:

1. Open the MessageDatabase asset
2. Increase the Messages array size
3. For each message, fill in:
   - Sender: Who sent the message
   - Subject: Email subject line (for emails only)
   - Content: The message body
   - Is Scam: True if it's a scam, false if legitimate
   - Explanation: Why it's a scam or legitimate
   - Message Type: Email, SMS, or Call

## Common Scam Patterns Included

- Phishing emails with fake URLs
- Fake tax refund scams
- Bank impersonation with typos in domains
- Smishing (SMS phishing) with urgent threats
- Cryptocurrency payment requests
- IRS impersonation calls
- Fake fraud department calls asking for SSN

## Next Steps

1. Create 3D models for office, PC monitor, and phone
2. Design and style the UI canvases
3. Add sound effects for calls and notifications
4. Implement scoring system
5. Add multiple difficulty levels
6. Create more diverse scam examples
