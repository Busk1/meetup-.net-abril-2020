import * as React from 'react';
import { connect } from 'react-redux';
import axios from 'axios';

export class Home extends React.Component<any,any> {

    constructor(props: any) {
        super(props);

        this.state = {
            dependencies: {
                service: {}
            }
        }
    }    

    async componentDidMount() {
        var url = new URL(window.location.href + "api/dependencies/dependencyinjection");
        let response = await axios.get(url.href);
        this.setState({
            dependencies: response.data
        });
    }

    render() {
        return (
            <>
                <div className="container">
                    <div className="row">
                        <div className="col-12">
                            <b> Desde la inyeccion </b>
                        </div>
                        <div className="col-12">
                            Transient ==> {this.state.dependencies.transient}
                        </div>
                        <div className="col-12">
                            Scoped ==> {this.state.dependencies.scoped}
                        </div>
                        <div className="col-12">
                            Singleton ==> {this.state.dependencies.singleton}
                        </div>
                    </div>
                    <hr />
                    <div className="row">
                        <div className="col-12">
                            <b> Desde el servicio </b>
                        </div>
                        <div className="col-12">
                            Transient ==> {this.state.dependencies.service.transient}
                        </div>
                        <div className="col-12">
                            Scoped ==> {this.state.dependencies.service.scoped}
                        </div>
                        <div className="col-12">
                            Singleton ==> {this.state.dependencies.service.singleton}
                        </div>
                    </div>
                </div>
            </>
        )
    }
}
export default connect()(Home);
