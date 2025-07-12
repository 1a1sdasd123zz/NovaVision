using System.Collections.Generic;

namespace NovaVision.BaseClass.Communication.TCP
{
    public class TcpBlockElementsCollection
    {
        private TcpBlockElement triggerEnable;

        private TcpBlockElement setOffline;

        private TcpBlockElement initialJobLoad;

        private TcpBlockElement triggerCamera0;

        private TcpBlockElement triggerCamera1;

        private TcpBlockElement triggerCamera2;

        private TcpBlockElement triggerCamera3;

        private TcpBlockElement triggerCamera4;

        private TcpBlockElement triggerCamera5;

        private TcpBlockElement triggerCamera6;

        private TcpBlockElement triggerCamera7;

        private TcpBlockElement setUserData0;

        private TcpBlockElement setUserData1;

        private TcpBlockElement setUserData2;

        private TcpBlockElement setUserData3;

        private TcpBlockElement setUserData4;

        private TcpBlockElement setUserData5;

        private TcpBlockElement setUserData6;

        private TcpBlockElement setUserData7;

        private TcpBlockElement systemReady;

        private TcpBlockElement systemBusy;

        private TcpBlockElement online;

        private TcpBlockElement jobLoadCompleted;

        private TcpBlockElement triggerReadyCamera0;

        private TcpBlockElement triggerReadyCamera1;

        private TcpBlockElement triggerReadyCamera2;

        private TcpBlockElement triggerReadyCamera3;

        private TcpBlockElement triggerReadyCamera4;

        private TcpBlockElement triggerReadyCamera5;

        private TcpBlockElement triggerReadyCamera6;

        private TcpBlockElement triggerReadyCamera7;

        private TcpBlockElement triggerAckCamera0;

        private TcpBlockElement triggerAckCamera1;

        private TcpBlockElement triggerAckCamera2;

        private TcpBlockElement triggerAckCamera3;

        private TcpBlockElement triggerAckCamera4;

        private TcpBlockElement triggerAckCamera5;

        private TcpBlockElement triggerAckCamera6;

        private TcpBlockElement triggerAckCamera7;

        private TcpBlockElement inspectionCompleted0;

        private TcpBlockElement inspectionCompleted1;

        private TcpBlockElement inspectionCompleted2;

        private TcpBlockElement inspectionCompleted3;

        private TcpBlockElement inspectionCompleted4;

        private TcpBlockElement inspectionCompleted5;

        private TcpBlockElement inspectionCompleted6;

        private TcpBlockElement inspectionCompleted7;

        private TcpBlockElement setUserDataAck0;

        private TcpBlockElement setUserDataAck1;

        private TcpBlockElement setUserDataAck2;

        private TcpBlockElement setUserDataAck3;

        private TcpBlockElement setUserDataAck4;

        private TcpBlockElement setUserDataAck5;

        private TcpBlockElement setUserDataAck6;

        private TcpBlockElement setUserDataAck7;

        private List<TcpBlockElement> TcpBlockElements;

        public TcpBlockElement TriggerEnable => triggerEnable;

        public TcpBlockElement SetOffline => setOffline;

        public TcpBlockElement InitialJobLoad => initialJobLoad;

        public TcpBlockElement TriggerCamera0 => triggerCamera0;

        public TcpBlockElement TriggerCamera1 => triggerCamera1;

        public TcpBlockElement TriggerCamera2 => triggerCamera2;

        public TcpBlockElement TriggerCamera3 => triggerCamera3;

        public TcpBlockElement TriggerCamera4 => triggerCamera4;

        public TcpBlockElement TriggerCamera5 => triggerCamera5;

        public TcpBlockElement TriggerCamera6 => triggerCamera6;

        public TcpBlockElement TriggerCamera7 => triggerCamera7;

        public TcpBlockElement SetUserData0 => setUserData0;

        public TcpBlockElement SetUserData1 => setUserData1;

        public TcpBlockElement SetUserData2 => setUserData2;

        public TcpBlockElement SetUserData3 => setUserData3;

        public TcpBlockElement SetUserData4 => setUserData4;

        public TcpBlockElement SetUserData5 => setUserData5;

        public TcpBlockElement SetUserData6 => setUserData6;

        public TcpBlockElement SetUserData7 => setUserData7;

        public TcpBlockElement SystemReady => systemReady;

        public TcpBlockElement SystemBusy => systemBusy;

        public TcpBlockElement Online => online;

        public TcpBlockElement JobLoadCompleted => jobLoadCompleted;

        public TcpBlockElement TriggerReadyCamera0 => triggerReadyCamera0;

        public TcpBlockElement TriggerReadyCamera1 => triggerReadyCamera1;

        public TcpBlockElement TriggerReadyCamera2 => triggerReadyCamera2;

        public TcpBlockElement TriggerReadyCamera3 => triggerReadyCamera3;

        public TcpBlockElement TriggerReadyCamera4 => triggerReadyCamera4;

        public TcpBlockElement TriggerReadyCamera5 => triggerReadyCamera5;

        public TcpBlockElement TriggerReadyCamera6 => triggerReadyCamera6;

        public TcpBlockElement TriggerReadyCamera7 => triggerReadyCamera7;

        public TcpBlockElement TriggerAckCamera0 => triggerAckCamera0;

        public TcpBlockElement TriggerAckCamera1 => triggerAckCamera1;

        public TcpBlockElement TriggerAckCamera2 => triggerAckCamera2;

        public TcpBlockElement TriggerAckCamera3 => triggerAckCamera3;

        public TcpBlockElement TriggerAckCamera4 => triggerAckCamera4;

        public TcpBlockElement TriggerAckCamera5 => triggerAckCamera5;

        public TcpBlockElement TriggerAckCamera6 => triggerAckCamera6;

        public TcpBlockElement TriggerAckCamera7 => triggerAckCamera7;

        public TcpBlockElement InspectionCompleted0 => inspectionCompleted0;

        public TcpBlockElement InspectionCompleted1 => inspectionCompleted1;

        public TcpBlockElement InspectionCompleted2 => inspectionCompleted2;

        public TcpBlockElement InspectionCompleted3 => inspectionCompleted3;

        public TcpBlockElement InspectionCompleted4 => inspectionCompleted4;

        public TcpBlockElement InspectionCompleted5 => inspectionCompleted5;

        public TcpBlockElement InspectionCompleted6 => inspectionCompleted6;

        public TcpBlockElement InspectionCompleted7 => inspectionCompleted7;

        public TcpBlockElement SetUserDataAck0 => setUserDataAck0;

        public TcpBlockElement SetUserDataAck1 => setUserDataAck1;

        public TcpBlockElement SetUserDataAck2 => setUserDataAck2;

        public TcpBlockElement SetUserDataAck3 => setUserDataAck3;

        public TcpBlockElement SetUserDataAck4 => setUserDataAck4;

        public TcpBlockElement SetUserDataAck5 => setUserDataAck5;

        public TcpBlockElement SetUserDataAck6 => setUserDataAck6;

        public TcpBlockElement SetUserDataAck7 => setUserDataAck7;

        public TcpBlockElement this[int index]
        {
            get
            {
                if (index < 0 || index > TcpBlockElements.Count - 1)
                {
                    return null;
                }
                return TcpBlockElements[index];
            }
        }

        public TcpBlockElementsCollection()
        {
            triggerEnable = new TcpBlockElement
            {
                Index = 0,
                ByteOffset = 0,
                Value = 0
            };
            setOffline = new TcpBlockElement
            {
                Index = 1,
                ByteOffset = 1,
                Value = 0
            };
            initialJobLoad = new TcpBlockElement
            {
                Index = 2,
                ByteOffset = 2,
                Value = 0
            };
            triggerCamera0 = new TcpBlockElement
            {
                Index = 3,
                ByteOffset = 5,
                Value = 0
            };
            triggerCamera1 = new TcpBlockElement
            {
                Index = 4,
                ByteOffset = 7,
                Value = 0
            };
            triggerCamera2 = new TcpBlockElement
            {
                Index = 5,
                ByteOffset = 9,
                Value = 0
            };
            triggerCamera3 = new TcpBlockElement
            {
                Index = 6,
                ByteOffset = 11,
                Value = 0
            };
            triggerCamera4 = new TcpBlockElement
            {
                Index = 7,
                ByteOffset = 13,
                Value = 0
            };
            triggerCamera5 = new TcpBlockElement
            {
                Index = 8,
                ByteOffset = 15,
                Value = 0
            };
            triggerCamera6 = new TcpBlockElement
            {
                Index = 9,
                ByteOffset = 17,
                Value = 0
            };
            triggerCamera7 = new TcpBlockElement
            {
                Index = 10,
                ByteOffset = 19,
                Value = 0
            };
            setUserData0 = new TcpBlockElement
            {
                Index = 11,
                ByteOffset = 6,
                Value = 0
            };
            setUserData1 = new TcpBlockElement
            {
                Index = 12,
                ByteOffset = 8,
                Value = 0
            };
            setUserData2 = new TcpBlockElement
            {
                Index = 13,
                ByteOffset = 10,
                Value = 0
            };
            setUserData3 = new TcpBlockElement
            {
                Index = 14,
                ByteOffset = 12,
                Value = 0
            };
            setUserData4 = new TcpBlockElement
            {
                Index = 15,
                ByteOffset = 14,
                Value = 0
            };
            setUserData5 = new TcpBlockElement
            {
                Index = 16,
                ByteOffset = 16,
                Value = 0
            };
            setUserData6 = new TcpBlockElement
            {
                Index = 17,
                ByteOffset = 18,
                Value = 0
            };
            setUserData7 = new TcpBlockElement
            {
                Index = 18,
                ByteOffset = 20,
                Value = 0
            };
            systemReady = new TcpBlockElement
            {
                Index = 19,
                ByteOffset = 0,
                Value = 1
            };
            systemBusy = new TcpBlockElement
            {
                Index = 20,
                ByteOffset = 1,
                Value = 0
            };
            online = new TcpBlockElement
            {
                Index = 21,
                ByteOffset = 2,
                Value = 0
            };
            jobLoadCompleted = new TcpBlockElement
            {
                Index = 22,
                ByteOffset = 3,
                Value = 0
            };
            triggerReadyCamera0 = new TcpBlockElement
            {
                Index = 23,
                ByteOffset = 6,
                Value = 1
            };
            triggerReadyCamera1 = new TcpBlockElement
            {
                Index = 24,
                ByteOffset = 10,
                Value = 1
            };
            triggerReadyCamera2 = new TcpBlockElement
            {
                Index = 25,
                ByteOffset = 14,
                Value = 1
            };
            triggerReadyCamera3 = new TcpBlockElement
            {
                Index = 26,
                ByteOffset = 18,
                Value = 1
            };
            triggerReadyCamera4 = new TcpBlockElement
            {
                Index = 27,
                ByteOffset = 22,
                Value = 1
            };
            triggerReadyCamera5 = new TcpBlockElement
            {
                Index = 28,
                ByteOffset = 26,
                Value = 1
            };
            triggerReadyCamera6 = new TcpBlockElement
            {
                Index = 29,
                ByteOffset = 30,
                Value = 1
            };
            triggerReadyCamera7 = new TcpBlockElement
            {
                Index = 30,
                ByteOffset = 34,
                Value = 1
            };
            triggerAckCamera0 = new TcpBlockElement
            {
                Index = 31,
                ByteOffset = 7,
                Value = 0
            };
            triggerAckCamera1 = new TcpBlockElement
            {
                Index = 32,
                ByteOffset = 11,
                Value = 0
            };
            triggerAckCamera2 = new TcpBlockElement
            {
                Index = 33,
                ByteOffset = 15,
                Value = 0
            };
            triggerAckCamera3 = new TcpBlockElement
            {
                Index = 34,
                ByteOffset = 19,
                Value = 0
            };
            triggerAckCamera4 = new TcpBlockElement
            {
                Index = 35,
                ByteOffset = 23,
                Value = 0
            };
            triggerAckCamera5 = new TcpBlockElement
            {
                Index = 36,
                ByteOffset = 27,
                Value = 0
            };
            triggerAckCamera6 = new TcpBlockElement
            {
                Index = 37,
                ByteOffset = 31,
                Value = 0
            };
            triggerAckCamera7 = new TcpBlockElement
            {
                Index = 38,
                ByteOffset = 35,
                Value = 0
            };
            inspectionCompleted0 = new TcpBlockElement
            {
                Index = 39,
                ByteOffset = 8,
                Value = 0
            };
            inspectionCompleted1 = new TcpBlockElement
            {
                Index = 40,
                ByteOffset = 12,
                Value = 0
            };
            inspectionCompleted2 = new TcpBlockElement
            {
                Index = 41,
                ByteOffset = 16,
                Value = 0
            };
            inspectionCompleted3 = new TcpBlockElement
            {
                Index = 42,
                ByteOffset = 20,
                Value = 0
            };
            inspectionCompleted4 = new TcpBlockElement
            {
                Index = 43,
                ByteOffset = 24,
                Value = 0
            };
            inspectionCompleted5 = new TcpBlockElement
            {
                Index = 44,
                ByteOffset = 28,
                Value = 0
            };
            inspectionCompleted6 = new TcpBlockElement
            {
                Index = 45,
                ByteOffset = 32,
                Value = 0
            };
            inspectionCompleted7 = new TcpBlockElement
            {
                Index = 46,
                ByteOffset = 36,
                Value = 0
            };
            setUserDataAck0 = new TcpBlockElement
            {
                Index = 47,
                ByteOffset = 9,
                Value = 0
            };
            setUserDataAck1 = new TcpBlockElement
            {
                Index = 48,
                ByteOffset = 13,
                Value = 0
            };
            setUserDataAck2 = new TcpBlockElement
            {
                Index = 49,
                ByteOffset = 17,
                Value = 0
            };
            setUserDataAck3 = new TcpBlockElement
            {
                Index = 50,
                ByteOffset = 21,
                Value = 0
            };
            setUserDataAck4 = new TcpBlockElement
            {
                Index = 51,
                ByteOffset = 25,
                Value = 0
            };
            setUserDataAck5 = new TcpBlockElement
            {
                Index = 52,
                ByteOffset = 29,
                Value = 0
            };
            setUserDataAck6 = new TcpBlockElement
            {
                Index = 53,
                ByteOffset = 33,
                Value = 0
            };
            setUserDataAck7 = new TcpBlockElement
            {
                Index = 54,
                ByteOffset = 37,
                Value = 0
            };
            TcpBlockElements = new List<TcpBlockElement>();
            TcpBlockElements.Add(TriggerEnable);
            TcpBlockElements.Add(SetOffline);
            TcpBlockElements.Add(InitialJobLoad);
            TcpBlockElements.Add(TriggerCamera0);
            TcpBlockElements.Add(TriggerCamera1);
            TcpBlockElements.Add(TriggerCamera2);
            TcpBlockElements.Add(TriggerCamera3);
            TcpBlockElements.Add(TriggerCamera4);
            TcpBlockElements.Add(TriggerCamera5);
            TcpBlockElements.Add(TriggerCamera6);
            TcpBlockElements.Add(TriggerCamera7);
            TcpBlockElements.Add(SetUserData0);
            TcpBlockElements.Add(SetUserData1);
            TcpBlockElements.Add(SetUserData2);
            TcpBlockElements.Add(SetUserData3);
            TcpBlockElements.Add(SetUserData4);
            TcpBlockElements.Add(SetUserData5);
            TcpBlockElements.Add(SetUserData6);
            TcpBlockElements.Add(SetUserData7);
            TcpBlockElements.Add(SystemReady);
            TcpBlockElements.Add(SystemBusy);
            TcpBlockElements.Add(Online);
            TcpBlockElements.Add(JobLoadCompleted);
            TcpBlockElements.Add(TriggerReadyCamera0);
            TcpBlockElements.Add(TriggerReadyCamera1);
            TcpBlockElements.Add(TriggerReadyCamera2);
            TcpBlockElements.Add(TriggerReadyCamera3);
            TcpBlockElements.Add(TriggerReadyCamera4);
            TcpBlockElements.Add(TriggerReadyCamera5);
            TcpBlockElements.Add(TriggerReadyCamera6);
            TcpBlockElements.Add(TriggerReadyCamera7);
            TcpBlockElements.Add(TriggerAckCamera0);
            TcpBlockElements.Add(TriggerAckCamera1);
            TcpBlockElements.Add(TriggerAckCamera2);
            TcpBlockElements.Add(TriggerAckCamera3);
            TcpBlockElements.Add(TriggerAckCamera4);
            TcpBlockElements.Add(TriggerAckCamera5);
            TcpBlockElements.Add(TriggerAckCamera6);
            TcpBlockElements.Add(TriggerAckCamera7);
            TcpBlockElements.Add(InspectionCompleted0);
            TcpBlockElements.Add(InspectionCompleted1);
            TcpBlockElements.Add(InspectionCompleted2);
            TcpBlockElements.Add(InspectionCompleted3);
            TcpBlockElements.Add(InspectionCompleted4);
            TcpBlockElements.Add(InspectionCompleted5);
            TcpBlockElements.Add(InspectionCompleted6);
            TcpBlockElements.Add(InspectionCompleted7);
            TcpBlockElements.Add(SetUserDataAck0);
            TcpBlockElements.Add(SetUserDataAck1);
            TcpBlockElements.Add(SetUserDataAck2);
            TcpBlockElements.Add(SetUserDataAck3);
            TcpBlockElements.Add(SetUserDataAck4);
            TcpBlockElements.Add(SetUserDataAck5);
            TcpBlockElements.Add(SetUserDataAck6);
            TcpBlockElements.Add(SetUserDataAck7);
        }
    }
}
