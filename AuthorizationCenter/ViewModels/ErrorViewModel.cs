using System;

namespace AuthorizationCenter.ViewModels
{
    /// <summary>
    /// ������ͼģ��
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// �Ƿ���ʾ����ID
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}