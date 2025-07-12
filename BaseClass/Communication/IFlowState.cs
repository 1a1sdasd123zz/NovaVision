using System;
using System.Collections.Generic;
using NovaVision.BaseClass.Communication.CommData;
using NovaVision.BaseClass.Communication.TCP;
using NovaVision.BaseClass.Module;

namespace NovaVision.BaseClass.Communication
{
    public delegate void ConnectedEventHandler(object sender, bool isConnected);
    public delegate void TriggerEventHandler(object sender, string triggerPoint, string triggeNum);
    public interface IFlowState
    {
        string SerialNum { get; }

        bool IsConnected { get; }

        EndianEnum Endian { get; set; }

        string CommTypeName { get; }

        event Action<int> JobChanged;

        event ConnectedEventHandler CommConnected;

        event TriggerEventHandler Trigger;

        void SetInputsOutputs(InputsOutputs<Comm_Element, Communication.CommData.Info> commConfigData);

        void SendCommElements(List<Comm_Element> elements, string remoteIp = null);

        void SendData(string data);

        void ResetSystemState();

        void AcqCompeleted(Comm_Element comm_Element);

        void InspectCompeleted(List<Comm_Element> comm_Element);

        void JobChangeCompeleted(int currentJobId);

        void SystemOffLine();

        void SystemOnLine();

        void Close();
    }
}
