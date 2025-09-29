// 代码生成时间: 2025-09-30 02:24:30
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace DataSyncTool
{
    public class DataSyncTool : ComponentBase
    {
        [Inject]
        private IDataSynchronizationService DataSynchronizationService { get; set; }

        private string _sourceData;
        private string _targetData;
        private bool _isSynchronizing;
        private string _syncStatus;

        // Method to initiate data synchronization
        public async Task SyncDataAsync()
        {
            _isSynchronizing = true;
            _syncStatus = "Synchronization started...";
            StateHasChanged();

            try
            {
                // Call the data synchronization service to perform the sync
                await DataSynchronizationService.SyncDataAsync(_sourceData, _targetData);

                _syncStatus = "Synchronization completed successfully.";
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during synchronization
                _syncStatus = $"Synchronization failed: {ex.Message}";
            }
            finally
            {
                _isSynchronizing = false;
                StateHasChanged();
            }
        }

        // Method to set the source data for synchronization
        public void SetSourceData(string data)
        {
            _sourceData = data;
            SyncStatus = "Ready to synchronize...";
        }

        // Method to set the target data for synchronization
        public void SetTargetData(string data)
        {
            _targetData = data;
            SyncStatus = "Ready to synchronize...";
        }

        // Property to get the current synchronization status
        public string SyncStatus
        {
            get => _syncStatus;
            private set
            {
                _syncStatus = value;
                StateHasChanged();
            }
        }

        // Property to check if synchronization is in progress
        public bool IsSynchronizing
        {
            get => _isSynchronizing;
            private set => _isSynchronizing = value;
        }
    }
}
