"use client";

import { useState } from "react";

import {
  Box,
  Button,
  Divider,
  Drawer,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Stack,
  Typography,
  useColorScheme,
} from "@mui/material";
import {
  SettingsOutlined as SettingsIcon,
  CloseOutlined as CloseIcon,
} from "@mui/icons-material";

import { useLayoutActions } from "@/stores/hooks";

import StyledThemeModeSwitch from "../common/StyledThemeModeSwitch";
import { PrimaryColors } from "@/theme/theme";

const AppSettingsDrawerButton = () => {
  const { mode, setMode } = useColorScheme();
  const layoutActions = useLayoutActions();
  const [visible, setVisible] = useState(false);

  const open = () => setVisible(true);
  const close = () => setVisible(false);

  const toggleTheme = () => {
    setMode(mode === "light" ? "dark" : "light");
  };

  return (
    <>
      <IconButton onClick={open}>
        <SettingsIcon />
      </IconButton>
      <Drawer
        anchor="right"
        open={visible}
        onClose={close}
        PaperProps={{
          sx: {
            width: 280,
          },
        }}
      >
        <Stack
          direction="row"
          justifyContent="space-between"
          alignItems="center"
          padding={2}
          paddingRight={1}
        >
          <Typography variant="h6" fontWeight="bold">
            Settings
          </Typography>
          <IconButton onClick={close}>
            <CloseIcon />
          </IconButton>
        </Stack>
        <Divider />
        <List disablePadding>
          <ListItemButton
            color="primary"
            sx={{
              padding: 3,
            }}
            onClick={toggleTheme}
          >
            <ListItemText
              primaryTypographyProps={{
                component: "h6",
                variant: "subtitle2",
                fontWeight: "bold",
                sx: (theme) => ({
                  [theme.getColorSchemeSelector("dark")]: {
                    color: theme.vars.palette.primary.main,
                  },
                }),
              }}
              primary="Mode"
            />
            <ListItemIcon>
              <StyledThemeModeSwitch checked={mode === "light"} readOnly />
            </ListItemIcon>
          </ListItemButton>
        </List>
        {/* <Box>
          <Typography
            padding={3}
            component="h6"
            variant="subtitle2"
            fontWeight="bold"
          >
            Presets
          </Typography>

          <Stack>
            {PrimaryColors.map((item) => {
              return (
                <Button
                  key={item}
                  sx={{ color: item }}
                  onClick={() => layoutActions.changePalettePrimary(item)}
                >
                  item
                </Button>
              );
            })}
          </Stack>
        </Box> */}
      </Drawer>
    </>
  );
};

export default AppSettingsDrawerButton;
