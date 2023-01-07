import '@testing-library/jest-dom';
import { fireEvent, render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { wait } from '@testing-library/user-event/dist/utils';
import { act } from 'react-dom/test-utils';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import Swal from 'sweetalert2';
import { vi } from 'vitest';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import LoginForm from '../../Components/Account/Login/LoginForm';
import { store } from '../../store';

vi.mock('axios', () => ({
  default: {
    create: () => ({
      interceptors: {
        response: {
          use: () => ({}),
        },
        request: {
          use: () => ({}),
        },
      },
    }),
  },
}));

describe('<LoginForm/>', () => {
  it('Should disable login button while logging in', async () => {
    vi.spyOn(AccountsProxy, 'logIn').mockImplementationOnce(
      async () => (await wait(1000)) as any
    );

    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <LoginForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password'), 'fake@email.on');
      fireEvent.click(screen.getByRole('button', { name: 'logIn' }));
    });
    expect(screen.getByRole('button', { name: 'logIn' })).toBeDisabled();
  });

  it('Should validate filled email empty', async () => {
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <LoginForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      userEvent.type(screen.getByTestId('password'), 'fake@email.on');
      fireEvent.click(screen.getByRole('button', { name: 'logIn' }));
    });
    expect(screen.getByRole('textbox', { name: 'email' })).toHaveClass(
      'is-invalid'
    );
  });

  it('Should validate filled password empty', async () => {
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <LoginForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'email' }),
        'fake@email.on'
      );
      fireEvent.click(screen.getByRole('button', { name: 'logIn' }));
    });
    expect(screen.getByTestId('password')).toHaveClass('is-invalid');
  });

  it('Should show login failed', async () => {
    vi.spyOn(AccountsProxy, 'logIn').mockRejectedValueOnce({
      response: { status: 400 },
    });
    const swalMock = vi
      .spyOn(Swal, 'fire')
      .mockImplementation(async (e: any) => {
        expect(e.icon).toEqual('error');
        return await Promise.resolve({} as any);
      });
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <LoginForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password'), 'fake@email.on');
      fireEvent.click(screen.getByRole('button', { name: 'logIn' }));
    });

    expect(swalMock).toBeCalledTimes(1);
  });

  it('Should show account inactive', async () => {
    vi.spyOn(AccountsProxy, 'logIn').mockRejectedValueOnce({
      response: { status: 302 },
    });
    const swalMock = vi
      .spyOn(Swal, 'fire')
      .mockImplementation(async (e: any) => {
        expect(e.icon).toEqual('warning');
        return await Promise.resolve({} as any);
      });
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <LoginForm />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password'), 'fake@email.on');
      fireEvent.click(screen.getByRole('button', { name: 'logIn' }));
    });

    expect(swalMock).toBeCalledTimes(1);
  });
});
