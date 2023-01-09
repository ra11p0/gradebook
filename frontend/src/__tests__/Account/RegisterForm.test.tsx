import '@testing-library/jest-dom';
import { fireEvent, render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { wait } from '@testing-library/user-event/dist/utils';
import { act } from 'react-dom/test-utils';
import { ReactNotifications } from 'react-notifications-component';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { vi } from 'vitest';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import RegisterForm from '../../Components/Account/Register/RegisterForm';
import Notifications from '../../Notifications/Notifications';
import { store } from '../../store';

describe('<RegisterForm/>', () => {
  it('Should disable register button while registering', async () => {
    vi.spyOn(AccountsProxy, 'register').mockImplementationOnce((async () => {
      await wait(1000);
    }) as any);

    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <RegisterForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password'), 'fake@emaI2l.on');
      userEvent.type(screen.getByTestId('password2'), 'fake@emaI2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', { name: 'termsAndConditions' })
      );
      fireEvent.click(
        screen.getByRole('button', { name: 'registerButtonLabel' })
      );
    });
    expect(
      screen.getByRole('button', { name: 'registerButtonLabel' })
    ).toBeDisabled();
  });

  it('Should validate filled email empty', async () => {
    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <RegisterForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.type(screen.getByTestId('password'), 'fake@emaI2l.on');
      userEvent.type(screen.getByTestId('password2'), 'fake@emaI2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', { name: 'termsAndConditions' })
      );
      fireEvent.click(
        screen.getByRole('button', { name: 'registerButtonLabel' })
      );
    });
    expect(screen.getByRole('textbox', { name: 'email' })).toHaveClass(
      'is-invalid'
    );
  });

  it('Should validate filled password empty', async () => {
    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <RegisterForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password2'), 'fake@emaI2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', { name: 'termsAndConditions' })
      );
      fireEvent.click(
        screen.getByRole('button', { name: 'registerButtonLabel' })
      );
    });
    expect(screen.getByTestId('password')).toHaveClass('is-invalid');
  });

  it('Should validate filled passwords not matched', async () => {
    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <RegisterForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password'), 'fake@emaI2l.on');
      userEvent.type(screen.getByTestId('password2'), 'fake@emaId2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', {
          name: 'termsAndConditions',
        })
      );
      fireEvent.click(
        screen.getByRole('button', { name: 'registerButtonLabel' })
      );
    });
    expect(screen.getByTestId('password2')).toHaveClass('is-invalid');
  });

  it('Should validate filled password empty', async () => {
    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <RegisterForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password2'), 'fake@emaI2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', { name: 'termsAndConditions' })
      );
      fireEvent.click(
        screen.getByRole('button', { name: 'registerButtonLabel' })
      );
    });
    expect(screen.getByTestId('password')).toHaveClass('is-invalid');
  });

  it('Should show error alert', async () => {
    const mockekdAccountsProxy = vi
      .spyOn(AccountsProxy, 'register')
      .mockRejectedValueOnce({ response: { data: 'fakeError' } });
    const mockedNotifications = vi.spyOn(Notifications, 'showApiError');
    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <ReactNotifications />
            <RegisterForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password'), 'fake@emaI2l.on');
      userEvent.type(screen.getByTestId('password2'), 'fake@emaI2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', { name: 'termsAndConditions' })
      );

      fireEvent.click(
        screen.getByRole('button', { name: 'registerButtonLabel' })
      );
    });
    expect(mockekdAccountsProxy).toBeCalledTimes(1);
    expect(mockedNotifications).toBeCalledTimes(1);
  });
});
