import React from 'react';
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Account/accountActions';

const mapStateToProps = (state: any) => ({
      isLoggedIn: state.common.isLoggedIn
});
  
const mapDispatchToProps = (dispatch: any) => ({
    onLogIn: () => dispatch(logIn)
});

interface AbsenceProps{
    onLogIn?: ()=>{},
    isLoggedIn: boolean
}

class Absence extends React.Component<AbsenceProps> {
    render(): React.ReactNode {
        return (
            <div>
                d-board Absence
            </div>
          );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Absence);
