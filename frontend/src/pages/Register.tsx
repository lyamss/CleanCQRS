'use client'

import * as React from 'react';
import { Loader2 } from 'lucide-react'
import Loader1 from "@/components/Loading/Loader1";
import { SetLoginAndRegisterUserClassicDto } from "@/services/modelsDto/Users";
import { useState } from "react";
import { Lock, AtSignIcon as AtSymbolIcon } from 'lucide-react'
import Snackbar from '@mui/material/Snackbar';
import { UseUser } from '@/services/UseUser';
import { UseSnakeBarModal } from '@/services/UseSnakeBarModal';
import { AuthProvider } from '@/services/AuthProvider';
import { 
  Box, 
  TextField, 
  Button, 
  Typography, 
  Container, 
  Paper, 
  InputAdornment,
  Link,
} from '@mui/material';
import { styled } from '@mui/system';












const Page = () =>
{

  const { isLoadingApiFetchLoginAndRegister, AuthRegister, MessageApiAuth } = UseUser();
  const {open} = UseSnakeBarModal(isLoadingApiFetchLoginAndRegister, MessageApiAuth);

    const [formData, setFormData] = useState<SetLoginAndRegisterUserClassicDto>({
      password: "",
      email: "",
    })

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
      setFormData({ ...formData, [e.target.name]: e.target.value })
    }

    const handleRegister = () => {
        AuthRegister(formData);
      };

      return (
          <div className="min-h-screen flex items-center justify-center bg-gray-100">
        <AuthProvider
          LoadingComponent={<Loader1 />} 
          isProtected={false}
        >
          <Container component="main" maxWidth="xs">
            <StyledPaper elevation={6}>
              <Typography component="h1" variant="h4" gutterBottom>
                Register
              </Typography>
              <Box component="form" noValidate sx={{ mt: 1, width: '100%' }}>
                <StyledTextField
                  margin="normal"
                  required
                  fullWidth
                  id="email"
                  label="Email Address"
                  name="email"
                  autoComplete="email"
                  autoFocus
                  value={formData.email}
                  onChange={handleInputChange}
                  InputProps={{
                    startAdornment: (
                      <InputAdornment position="start">
                        <AtSymbolIcon style={{ width: 20, height: 20, color: 'rgba(0, 0, 0, 0.54)' }} />
                      </InputAdornment>
                    ),
                  }}
                />
                <StyledTextField
                  margin="normal"
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  autoComplete="current-password"
                  value={formData.password}
                  onChange={handleInputChange}
                  InputProps={{
                    startAdornment: (
                      <InputAdornment position="start">
                        <Lock style={{ width: 20, height: 20, color: 'rgba(0, 0, 0, 0.54)' }} />
                      </InputAdornment>
                    ),
                  }}
                />
                <StyledButton
                  fullWidth
                  variant="contained"
                  onClick={handleRegister}
                  disabled={isLoadingApiFetchLoginAndRegister}
                >
                  {isLoadingApiFetchLoginAndRegister ? <Loader2/> : "S'inscrire"}
                </StyledButton>
                <Box mt={2} textAlign="center">
                  <Link href="/Login" variant="body2" color="textPrimary">
                    {"Déjà un compte? Connexion"}
                  </Link>
                </Box>
              </Box>
            </StyledPaper>
            <Snackbar
              open={open}
              autoHideDuration={5000}
              message={MessageApiAuth}
            />
          </Container>
        </AuthProvider>
</div>
      );
}

export default Page;







const StyledPaper = styled(Paper)(({ theme }) => ({
    marginTop: theme.spacing(8),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    padding: theme.spacing(4),
    backgroundColor: 'white',
    borderRadius: '16px',
    boxShadow: '0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)',
  }));
  
  const StyledTextField = styled(TextField)(({ theme }) => ({
    '& .MuiOutlinedInput-root': {
      '& fieldset': {
        borderRadius: '12px',
      },
      '&:hover fieldset': {
        borderColor: 'rgba(0, 0, 0, 0.23)',
      },
      '&.Mui-focused fieldset': {
        borderColor: 'black',
      },
    },
  }));
  
  const StyledButton = styled(Button)(({ theme }) => ({
    borderRadius: '12px',
    backgroundColor: 'black',
    color: 'white',
    '&:hover': {
      backgroundColor: '#333',
    },
    padding: '10px 0',
    marginTop: theme.spacing(2),
  }));
