import React from 'react';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { Card, FormSelect } from 'react-bootstrap';
import { allUserRoles, UserRolesEnum }  from '../../Common/Enums/UserRolesEnum';

const mapStateToProps = (state: any) => ({
    
});
  
const mapDispatchToProps = (dispatch: any) => ({
    
});

interface ActivateAccountProps{
    t: any
}

interface ActivateAccountState{
    
}

class ActivateAccount extends React.Component<ActivateAccountProps, ActivateAccountState> {
    constructor(props: ActivateAccountProps) {
        super(props);
    }
    render(): React.ReactNode {
        const { t } = this.props;
        return (
            <Card className="m-3">
                <Card.Body>
                    <div>
                        <div className='display-6'>
                            {t('activateToUseGradebook')}
                        </div>
                        <FormSelect>
                            {
                                allUserRoles.map((e: UserRolesEnum)=>{
                                    return(
                                        <option key={e}>
                                            {t(e)}
                                        </option>
                                    )
                                })
                            }
                        </FormSelect>
                    </div>
                </Card.Body>
            </Card>
          );
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(ActivateAccount));
