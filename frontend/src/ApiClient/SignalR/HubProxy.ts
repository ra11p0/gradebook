import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import getSessionRedux from '../../Redux/ReduxQueries/account/getSessionRedux';

export default class HubProxy {
  private subscribers: Subscriber[] = [];
  private connection: HubConnection | undefined = undefined;

  public async connect(url: string): Promise<void> {
    if (this.connection) await this.connection.stop();
    const session = getSessionRedux();
    if (!session) return;
    this.connection = new HubConnectionBuilder()
      .withUrl(url, { accessTokenFactory: () => session.accessToken })
      .withAutomaticReconnect()
      .build();

    await this.connection.start().then(() => {
      this.subscribers.forEach((s) => {
        this.connection?.on(s.methodName, s.newMethod);
      });
    });
  }

  public async disconnect(): Promise<void> {
    if (this.connection) await this.connection.stop();
  }

  public async send(method: string, content: any): Promise<void> {
    await this.connection?.send(method, content);
  }

  public on(methodName: string, newMethod: (...args: any[]) => any): void {
    this.subscribers.push({ methodName, newMethod });
    this.connection?.on(methodName, newMethod);
  }

  public off(methodName: string): void {
    this.subscribers = this.subscribers.filter(
      (e) => e.methodName !== methodName
    );
    this.connection?.off(methodName);
  }
}

export interface Subscriber {
  methodName: string;
  newMethod: (...args: any[]) => any;
}
