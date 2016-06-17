﻿using System;
using UtahOpenSource.DataStore.Abstractions;
using Xamarin.Forms;
using System.Threading.Tasks;
using UtahOpenSource.DataObjects;
using System.Windows.Input;
using Plugin.Share;
using FormsToolkit;

namespace UtahOpenSource.Clients.Portable
{
    public class SessionDetailsViewModel : ViewModelBase
    {
        Session session;
        public Session Session
        {
            get { return session; }
            set { SetProperty(ref session, value); }
        }

       
        public SessionDetailsViewModel(INavigation navigation, Session session) : base(navigation)
        {
            Session = session;
            if (Session.StartTime.HasValue)
                ShowReminder = !Session.StartTime.Value.IsTBA();
            else
                ShowReminder = false;
        }


        public bool ShowReminder { get; set; }
     
        bool isReminderSet;
        public bool IsReminderSet
        {
            get { return isReminderSet; }
            set { SetProperty(ref isReminderSet, value); }
        } 



        Speaker selectedSpeaker;
        public Speaker SelectedSpeaker
        {
            get { return selectedSpeaker; }
            set
            {
                selectedSpeaker = value;
                OnPropertyChanged();
                if (selectedSpeaker == null)
                    return;

                MessagingService.Current.SendMessage(MessageKeys.NavigateToSpeaker, selectedSpeaker);

                SelectedSpeaker = null;
            }
        }


        ICommand  favoriteCommand;
        public ICommand FavoriteCommand =>
        favoriteCommand ?? (favoriteCommand = new Command(async () => await ExecuteFavoriteCommandAsync())); 

        async Task ExecuteFavoriteCommandAsync()
        {
            await FavoriteService.ToggleFavorite(Session);

        }

        ICommand  reminderCommand;
        public ICommand ReminderCommand =>
            reminderCommand ?? (reminderCommand = new Command(async () => await ExecuteReminderCommandAsync())); 

        async Task ExecuteReminderCommandAsync()
        {
            if(!IsReminderSet)
            {
                var result = await ReminderService.AddReminderAsync(Session.Id,
                    new Plugin.Calendars.Abstractions.CalendarEvent
                    {
                        AllDay = false,
                        Description = Session.Abstract,
                        Location = Session.Room?.Name ?? string.Empty,
                        Name = Session.Title,
                        Start = Session.StartTime.Value,
                        End = Session.EndTime.Value
                    });


                if(!result)
                    return;
                
                Logger.Track(EvolveLoggerKeys.ReminderAdded, "Title", Session.Title);
                IsReminderSet = true;
            }
            else
            {
                var result = await ReminderService.RemoveReminderAsync(Session.Id);
                if(!result)
                    return;
                Logger.Track(EvolveLoggerKeys.ReminderRemoved, "Title", Session.Title);
                IsReminderSet = false;
            }
                
        }

        ICommand  shareCommand;
        public ICommand ShareCommand =>
            shareCommand ?? (shareCommand = new Command(async () => await ExecuteShareCommandAsync())); 

        async Task ExecuteShareCommandAsync()
        {
            Logger.Track(EvolveLoggerKeys.Share, "Title", Session.Title);
            await CrossShare.Current.Share($"Can't wait for {Session.Title} at #UtahOpenSource!", "Share");
        }

        ICommand  loadSessionCommand;
        public ICommand LoadSessionCommand =>
            loadSessionCommand ?? (loadSessionCommand = new Command(async () => await ExecuteLoadSessionCommandAsync())); 

        public async Task ExecuteLoadSessionCommandAsync()
        {

            if(IsBusy)
                return;

            try 
            {
                

                IsBusy = true;
               
              
                IsReminderSet = await ReminderService.HasReminderAsync(Session.Id);
                Session.FeedbackLeft = await StoreManager.FeedbackStore.LeftFeedback(Session);


            } 
            catch (Exception ex) 
            {
                Logger.Report(ex, "Method", "ExecuteLoadSessionCommandAsync");
                MessagingService.Current.SendMessage(MessageKeys.Error, ex);
            }
            finally
            {
                IsBusy = false;
            }

        }

       

    }
}

