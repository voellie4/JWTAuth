import { Component } from '@angular/core';
import { AuthServiceService } from './auth-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: []
})
export class AppComponent {
  title = 'ClientApp';
  displayedMessage: string = "";

  constructor(private _authService: AuthServiceService) { }

  async button0_click() { 
    this.displayedMessage = await this._authService.getToken();
  }

  async button1_click() {
    try {
      const message = await this._authService.getAdminOnly()
      this.displayedMessage = message.displayedMessage;
      this.displayedMessage += " User needs to be admin";
    }
    catch (error : any) {
      this.displayedMessage = `Error: ${error.status}`;
      console.error(error);
    }
  }

  async button2_click() {
    try {
      const message = await this._authService.getAdminOrManager()
      this.displayedMessage = message.displayedMessage;
      this.displayedMessage += " User needs to be admin or manager";
    }
    catch (error: any) {
      this.displayedMessage = `Error: ${error.status}`;
      console.error(error);
    }
  }

  async button3_click() {
    try {
      const message = await this._authService.getAuthenticatedUser()
      this.displayedMessage = message.displayedMessage;
      this.displayedMessage += " User needs to be authenticated";
    }
    catch (error: any) {
      this.displayedMessage = `Error: ${error.status}`;
      console.error(error);
    }
  }

  async button4_click() {
    try {
      const message = await this._authService.getMinimumAgeUser()
      this.displayedMessage = message.displayedMessage;
      this.displayedMessage += " The minimum age is 18.";
    }
    catch (error: any) {
      this.displayedMessage = `Error: ${error.status}`;
      console.error(error);
    }
  }
}


