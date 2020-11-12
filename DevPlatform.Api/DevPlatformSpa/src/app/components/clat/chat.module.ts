
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatBodyComponent } from './chat-body/chat-body.component';
import { ChatHeaderComponent } from './chat-header/chat-header.component';
import { ChatLayoutComponent } from './chat-layout/chat-layout.component';
import { ChatMessagesComponent } from './chat-messages/chat-messages.component';
import { ChatSidebarComponent } from './chat-sidebar/chat-sidebar.component';
import { ChatUserListComponent } from './chat-user-list/chat-user-list.component';
import { ChatUserComponent } from './chat-user/chat-user.component';

const chatRoutes: Routes = [
    { path: "chat", component: ChatLayoutComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(chatRoutes),
    ],
    declarations: [
        ChatLayoutComponent,
        ChatHeaderComponent,
        ChatBodyComponent,
        ChatSidebarComponent,
        ChatUserComponent,
        ChatUserListComponent,
        ChatMessagesComponent
    ]
})
export class ChatModule { }
